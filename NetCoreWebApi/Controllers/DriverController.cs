using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wasalee.ResponseFormats;
using DAL;
using Wasalee.BindingModels;
using BLL.Interface;
using Wasalee.Utility;
using Wasalee.DTOs;
using AutoMapper;
using Wasalee.JwtHelpers;
using Microsoft.Extensions.Configuration;

namespace Wasalee.Controllers
{
    [Produces("application/json")]
    [Route("api/Driver")]
    public class DriverController : Controller
    {
        #region Properties and Constructor
        public IConfiguration _configuration { get; }
        protected readonly DataContext _dbContext;
        protected readonly IBODriver _bODriver;

        public DriverController(DataContext dataContext, IConfiguration configuration, IBODriver bODriver)
        {
            _configuration = configuration;
            _dbContext = dataContext;
            _bODriver = bODriver;
        } 
        #endregion

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterDriverBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (_bODriver.Exists(model.Email))
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateAlreadyExists("License Number") } });

                var driver = new Driver
                {
                    Email = model.Email,
                    LicenseNo = model.LicenseNo,
                    FullName = model.FullName,
                    PhoneNo = model.PhoneNo,
                    DateOfBirth = model.DateOfBirth,
                    HomeAddress =model.HomeAddress,
                    Password = CryptoHelper.Hash(model.Password),
                    IsNotificationsOn = true,
                    SignInType = (int)UserTypes.Driver,
                    CreatedDate = DateTime.UtcNow
                };

                _bODriver.InsertDriver(driver);

                var driverDTO = Mapper.Map<Driver, DriverDTO>(driver);

                driverDTO.GenerateToken(_configuration);

                return Ok(new CustomResponse<DriverDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = driverDTO });

            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var driver = _bODriver.AuthenticateCredentials(model.username, CryptoHelper.Hash(model.password));

                if (driver != null)
                {
                    var driverDTO = Mapper.Map<Driver, DriverDTO>(driver);

                    driverDTO.GenerateToken(_configuration);

                    return Ok(new CustomResponse<DriverDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = driverDTO });
                }
                else
                {
                    return Ok(new CustomResponse<Error>
                    {
                        Message = Global.ResponseMessages.Forbidden,
                        StatusCode = StatusCodes.Status403Forbidden,
                        Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("username or password") }
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }
    }
}