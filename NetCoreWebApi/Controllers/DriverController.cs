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
using Microsoft.AspNetCore.Authorization;
using Wasalee.ViewModels;
using Wasalee.BindingModels.Account;
using Component.Models;
using Component.Utility.Enums;
using Component.Culture;
using Component.Models.Driver;

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

                //_bODriver.InsertDriver(driver);
                CultureType culture = CultureHelper.GetCulture(Request.HttpContext);
                var driver = _bODriver.InsertDriver(model, culture);
                if (driver != null)
                {
                    var driverDTO = Mapper.Map<Driver, DriverDTO>(driver);
                    //driverDTO = Mapper.Map(driver.DriverML.FirstOrDefault(x => x.Culture == culture), driverDTO);
                    driverDTO.GenerateToken(_configuration);
                    return Ok(new CustomResponse<DriverDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = driverDTO });
                }
                else
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Forbidden, StatusCode = StatusCodes.Status403Forbidden, Result = new Error { ErrorMessage = "Something went wrong! Try Again." } });
                }
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
                CultureType culture = CultureHelper.GetCulture(Request.HttpContext);
                if (driver != null)
                {
                    //var driverDTO = Mapper.Map<Driver, DriverDTO>(driver);
                    var driverDTO = Mapper.Map<Driver, DriverDTO>(driver);
                    //driverDTO = Mapper.Map(driver.DriverML.FirstOrDefault(x => x.Culture == culture), driverDTO);
                    driverDTO.GenerateToken(_configuration);
                    return Ok(new CustomResponse<DriverDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = driverDTO });
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

        [Authorize]
        [Route("GetAllRequests")]
        [HttpGet]
        public async Task<IActionResult> GetAllRequests(int Items = 5, int Page = 0, int? Type = 0)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                int Driver_Id = Convert.ToInt32(User.GetClaimValue("Id"));
                CultureType culture = CultureHelper.GetCulture(Request.HttpContext);

                PendingRequestViewModel pendingRequest = new PendingRequestViewModel();
                HistoryRequestViewModel historyRequest = new HistoryRequestViewModel();

                //RiderMyRequests returnModel = new RiderMyRequests();

                if (Type == 0)
                {
                    var AllRequests = _bODriver.GetAllRequests(Items, Page);
                    if (AllRequests != null)
                    {
                        Mapper.Map(AllRequests, pendingRequest.Pending);
                        for (int i = 0; i < AllRequests.Count; i++)
                        {
                            Mapper.Map(AllRequests[i].RequestItemML.FirstOrDefault(y => y.Culture == culture), pendingRequest.Pending[i]);
                            //Mapper.Map(AllRequests[i].Driver.DriverML.FirstOrDefault(y => y.Culture == culture), historyRequest.History[i].Driver);

                        }
                    }
                    else
                        pendingRequest.Pending = new List<RequestItemDTO>();

                    return Ok(new CustomResponse<PendingRequestViewModel> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = pendingRequest });
                }
                else
                {
                    var CompletedRequests = _bODriver.GetRiderRequests(Driver_Id, Items, Page);
                    if (CompletedRequests != null)
                    {
                        Mapper.Map(CompletedRequests, historyRequest.History);
                        for (int i = 0; i < CompletedRequests.Count; i++)
                        {
                            Mapper.Map(CompletedRequests[i].RequestItemML.FirstOrDefault(y => y.Culture == culture), historyRequest.History[i]);
                            //Mapper.Map(CompletedRequests[i].Driver.DriverML.FirstOrDefault(y => y.Culture == culture), historyRequest.History[i].Driver);

                        }
                    }
                    else
                        historyRequest.History = new List<RequestItemDTO>();

                    return Ok(new CustomResponse<HistoryRequestViewModel> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = historyRequest });
                }
                //returnModel.Pending = Mapper.Map<List<RequestItem>, List<RequestItemDTO>>(AllRequests);
                //returnModel.History = Mapper.Map<List<RequestItem>, List<RequestItemDTO>>(CompletedRequests);
                //return Ok(new CustomResponse<RiderMyRequests> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = returnModel });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }

        [Authorize]
        [Route("CancelBooking")]
        [HttpGet]
        public async Task<IActionResult> CancelBooking(int Request_Id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                int Driver_Id = Convert.ToInt32(User.GetClaimValue("Id"));

                var response = _bODriver.CancelBooking(Driver_Id, Request_Id);
                if (response)
                    return Ok(new CustomResponse<string> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = "Your booking has been cancelled." });
                else
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Forbidden, StatusCode = StatusCodes.Status403Forbidden, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("request id") } });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }

        [Authorize]
        [Route("AcceptRequest")]
        [HttpGet]
        public async Task<IActionResult> AcceptRequest(int Request_Id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                RequestItemViewModel returnModel = new RequestItemViewModel();
                int Driver_Id = Convert.ToInt32(User.GetClaimValue("Id"));
                CultureType culture = CultureHelper.GetCulture(Request.HttpContext);



                var response = _bODriver.AcceptRequest(Driver_Id, Request_Id);
                if (response != null)
                {
                    returnModel.Request = Mapper.Map<RequestItem, RequestItemDTO>(response);
                    Mapper.Map(response.RequestItemML.FirstOrDefault(x => x.Culture == culture), returnModel.Request);
                    return Ok(new CustomResponse<RequestItemViewModel> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = returnModel });
                }
                else
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Forbidden, StatusCode = StatusCodes.Status403Forbidden, Result = new Error { ErrorMessage = "Request already assigned." } });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }

        [Authorize]
        [Route("UpdateAvailabilityStatus")]
        [HttpGet]
        public async Task<IActionResult> UpdateAvailabilityStatus(bool IsAvailable)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                int Driver_Id = Convert.ToInt32(User.GetClaimValue("Id"));
                var response = _bODriver.UpdateAvailabilityStatus(Driver_Id, IsAvailable);

                if (response != null)
                {
                    var driver = Mapper.Map<Driver, DriverDTO>(response);
                    return Ok(new CustomResponse<DriverDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = driver });
                }
                else
                {
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Forbidden, StatusCode = StatusCodes.Status403Forbidden, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateNotFound("Driver") } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }

        [Authorize]
        [Route("UpdateNotificationStatus")]
        [HttpGet]
        public async Task<IActionResult> UpdateNotificationStatus(bool IsNotificationOn)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                int Driver_Id = Convert.ToInt32(User.GetClaimValue("Id"));
                var response = _bODriver.UpdateNotificationStatus(Driver_Id, IsNotificationOn);

                if (response)
                    return Ok(new CustomResponse<string> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = "Status updated successfully." });
                else
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Forbidden, StatusCode = StatusCodes.Status403Forbidden, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateNotFound("Driver") } });

            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }

        [HttpPost]
        [Route("ChangeDriverPassword")]
        public async Task<IActionResult> ChangeDriverPassword(ChangeDriverPasswordBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                int Driver_Id = Convert.ToInt32(User.GetClaimValue("Id"));

                var Driver = _bODriver.ChangeDriverPassword(Driver_Id, model.Password, model.NewPassword);

                if (Driver)
                {
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

        [Authorize]
        [Route("GetDriverProfile")]
        [HttpGet]
        public async Task<IActionResult> GetDriverProfile(int Driver_Id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                DriverDTO driverProfile = new DriverDTO();

                var Driver = _bODriver.GetDriverProfile(Driver_Id);

                if (Driver != null)
                {
                    driverProfile = Mapper.Map<Driver, DriverDTO>(Driver);
                    //Mapper.Map(Driver.DriverML.FirstOrDefault(x => x.Culture == CultureHelper.Culture), driverProfile);
                    return Ok(new CustomResponse<DriverDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = driverProfile });
                }
                else
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Forbidden, StatusCode = StatusCodes.Status403Forbidden, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateNotFound("Driver") } });

            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetDriverDetailsById")]
        public IActionResult GetDriverDetailsById(int Driver_Id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                CultureType culture = CultureHelper.GetCulture(Request.HttpContext);

                DriverDetailsViewModel responseModel = new DriverDetailsViewModel();


                var DriverDetails = _bODriver.GetDriverDetailsById(Driver_Id, culture);
                responseModel.Driver = Mapper.Map<Driver, DriverDTO>(DriverDetails);
                
                return Ok(new CustomResponse<DriverDetailsViewModel> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = responseModel });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }

        [Route("RateDriver")]
        [HttpPost]
        public async Task<IActionResult> RateDriver(RateDriverBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                model.User_Id = Convert.ToInt32(User.GetClaimValue("Id"));
                CultureType culture = CultureHelper.GetCulture(Request.HttpContext);
                DriverRatingViewModel response = new DriverRatingViewModel();

                var rateObj = _bODriver.RateDriver(model, culture);

                if (rateObj != null)
                {
                    response.Rating = Mapper.Map<DriverRating, DriverRatingDTO>(rateObj);

                    return Ok(new CustomResponse<DriverRatingViewModel> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = response });
                }
                else
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Forbidden, StatusCode = StatusCodes.Status403Forbidden, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateNotFound("Driver") } });

            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }

        [Route("ReportProblem")]
        [HttpPost]
        public async Task<IActionResult> ReportProblem(ReportProblemBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                model.User_Id = Convert.ToInt32(User.GetClaimValue("Id"));
                CultureType culture = CultureHelper.GetCulture(Request.HttpContext);

                //DriverRatingViewModel response = new DriverRatingViewModel();

                var rateObj = _bODriver.ReportProblem(model);

                if (rateObj)
                {
                    //response.Rating = Mapper.Map<DriverRating, DriverRatingDTO>(rateObj);
                    return Ok(new CustomResponse<string> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = "Problem reported successfully."});
                }
                else
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Forbidden, StatusCode = StatusCodes.Status403Forbidden, Result = new Error { ErrorMessage = "Something went wrong! Try again." } });

            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetDriverRatings")]
        public IActionResult GetDriverRatings(int Driver_Id,int? Items=3,int? Page=0)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                DriversRatingsViewModel responseModel = new DriversRatingsViewModel();


                var DriverRatings = _bODriver.GetDriverRatings(Driver_Id,Items,Page);
                responseModel.Ratings = Mapper.Map<List<DriverRating>, List<DriverRatingDTO>>(DriverRatings);
                responseModel.TotalRecords= _bODriver.GetTotalRatingsOfDriver(Driver_Id);

                return Ok(new CustomResponse<DriversRatingsViewModel> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = responseModel });

            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }

        //[Route("CancelOrder")]
        //[HttpPost]
        //public async Task<IActionResult> CancelOrder(CancelRequestBindingModel model)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //            return BadRequest(ModelState);

        //        model.Driver_Id = Convert.ToInt32(User.GetClaimValue("Id"));
        //        CultureType culture = CultureHelper.GetCulture(Request.HttpContext);
        //        //var response = CancelBooking(Driver_Id, Request_Id);
        //        var status = _bODriver.RateDriver(model, culture);

        //        if (status != null)
        //        {
        //            return Ok(new CustomResponse<string> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = "Review submitted successfully" });

        //        }
        //        else
        //        {
        //            return Ok(new CustomResponse<Error>
        //            {
        //                Message = Global.ResponseMessages.Forbidden,
        //                StatusCode = StatusCodes.Status403Forbidden,
        //                Result = new Error { ErrorMessage = "Something went wrong! Try again." }
        //            });
        //        }




        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(Error.LogError(ex));
        //    }
        //}

    }
}