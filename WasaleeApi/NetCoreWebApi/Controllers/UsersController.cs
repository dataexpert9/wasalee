using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wasalee.ResponseFormats;
using Wasalee.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using DAL;
using Wasalee.JwtHelpers;
using Microsoft.Extensions.Configuration;
using Wasalee.DTOs;
using BLL.Interface;
using Wasalee.BindingModels;
using AutoMapper;
using Wasalee.BindingModels.Account;


namespace NetCoreWebApi.Controllers
{
    [Route("/api/User")]
    public class UsersController : Controller
    {
        #region Properties and constructor
        public IConfiguration _configuration { get; }
        protected readonly DataContext _dbContext;
        protected readonly IBOUser _bOUser;
        protected readonly IBOSettings _bOSettings;


        public UsersController(DataContext dataContext, IConfiguration configuration, IBOUser bOUser,IBOSettings bOSettings)
        {
            _dbContext = dataContext;
            _configuration = configuration;
            _bOUser = bOUser;
            _bOSettings = bOSettings;
        }
        #endregion

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (_bOUser.Exists(model.Email))
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateAlreadyExists("Email") } });
                Random _rdm = new Random();
                var user = new User
                {
                    Email = model.Email,
                    FullName = model.FullName,
                    PhoneNo = model.PhoneNo,
                    Location = model.Location,
                    Password = CryptoHelper.Hash(model.Password),
                    IsNotificationsOn = true,
                    SignInType = 0,
                    CreatedDate = DateTime.UtcNow,
                    Status = Convert.ToInt16(Component.Utility.UserStatus.Inactive)
            };

                _bOUser.InsertUser(user);
                var userDTO = Mapper.Map<User, UserDTO>(user);

                userDTO.VerificationCode = _rdm.Next(1000, 9999).ToString();

                userDTO.GenerateToken(_configuration);

                return Ok(new CustomResponse<UserDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = userDTO });

            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }

        [HttpPost]
        [Route("VerifyUser")]
        public async Task<IActionResult> VerifyUser(VerifyUser model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = _bOUser.VerifyUser(model.User_Id);

                if (user != null)
                {
                    var userDTO = Mapper.Map<User, UserDTO>(user);

                    userDTO.GenerateToken(_configuration);

                    return Ok(new CustomResponse<UserDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = userDTO });
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



        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = _bOUser.AuthenticateCredentials(model.username, CryptoHelper.Hash(model.password));
                var settings = _bOSettings.LoadSettings();
                if (user != null)
                {
                    var userDTO = Mapper.Map<User, UserDTO>(user);

                    userDTO.GenerateToken(_configuration);

                    return Ok(new CustomResponse<UserDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = userDTO });
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


        //[HttpPost]
        //[Route("SocialLogin")]
        //public async Task<IActionResult> SocialLogin(string accessToken, int? socialLoginType)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //            return BadRequest(ModelState);

        //        var user = _bOUser.SocialLogin(accessToken,socialLoginType.Value);

        //        if (user != null)
        //        {
        //            var userDTO = Mapper.Map<User, UserDTO>(user);

        //            userDTO.GenerateToken(_configuration);

        //            return Ok(new CustomResponse<UserDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = userDTO });
        //        }
        //        else
        //        {
        //            return Ok(new CustomResponse<Error>
        //            {
        //                Message = Global.ResponseMessages.Forbidden,
        //                StatusCode = StatusCodes.Status403Forbidden,
        //                Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("username or password") }
        //            });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(Error.LogError(ex));
        //    }
        //}



        [HttpPost]
        [Route("ChangeUserPassword")]
        public async Task<IActionResult> ChangeUserPassword(ChangeUserPasswordBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = _bOUser.ChangeUserPassword(model.User_Id,model.Password,model.NewPassword);

                if (user)
                {
                    //var userDTO = Mapper.Map<User, UserDTO>(user);

                    //userDTO.GenerateToken(_configuration);

                    return Ok(new CustomResponse<string> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = "Password changed successfully." });
                }
                else
                {
                    return Ok(new CustomResponse<Error>
                    {
                        Message = Global.ResponseMessages.Forbidden,
                        StatusCode = StatusCodes.Status403Forbidden,
                        Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("email or password") }
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }


        [Route("LoginAsAdmin")]
        [HttpGet]
        public async Task<IActionResult> LoginAsAdmin(string username, string password)
        {
            if (username == "Admin" && password == "Pass")
            {
                var token = new JwtTokenBuilder()
                                .AddSecurityKey(JwtSecurityKey.Create(_configuration.GetValue<string>("JwtSecretKey")))
                                .AddIssuer(_configuration.GetValue<string>("JwtIssuer"))
                                .AddAudience(_configuration.GetValue<string>("JwtAudience"))
                                .AddExpiry(1)
                                .AddClaim("Name", "Admin")
                                .AddRole("Admin")
                                .Build();

                return Ok(new CustomResponse<string> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = token.Value });
            }
            else
                return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Forbidden, StatusCode = StatusCodes.Status403Forbidden, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("username or password") } });

        }

        [Route("GetUser")]
        [Authorize(Roles = "User, Admin")]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var name = User.GetClaimValue("Name");

            return Ok(new CustomResponse<string> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = "You are an authorized user" });
        }

        [Route("GetAdmin")]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAdmin()
        {
            var name = User.GetClaimValue("Name");

            return Ok(new CustomResponse<string> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = "You are an authorized user" });
        }

        [HttpGet]
        [Route("UpdateNotificationStatus")]
        public async Task<IActionResult> UpdateNotificationStatus(int User_Id,bool Status)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = _bOUser.UpdateNotificationStatus(User_Id,Status);

                if (user)
                {
                    return Ok(new CustomResponse<string> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = "Notification status updated successfully."});
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