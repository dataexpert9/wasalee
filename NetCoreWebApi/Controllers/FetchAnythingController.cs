﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Interface;
using Component.Culture;
using Component.ResponseFormats;
using Component.Utility;
using Component.Utility.Enums;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Wasalee.BindingModels.FetchAnything;
using Wasalee.DTOs;
using Wasalee.ViewModels;

namespace Wasalee.Controllers
{
    [Produces("application/json")]
    [Route("api/FetchAnything")]
    public class FetchAnythingController : Controller
    {
        public IConfiguration _configuration { get; }
        protected readonly IBOFetch _bOFetch;
        protected readonly DataContext _dbContext;

        public FetchAnythingController(DataContext dataContext, IConfiguration configuration, IBOFetch bOFetch)
        {
            _dbContext = dataContext;
            _configuration = configuration;
            _bOFetch = bOFetch;
        }

        [Authorize]
        [HttpPost]
        [Route("RequestItem")]
        public IActionResult RequestItem(RequestItemBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);



                model.User_Id = Convert.ToInt32(User.GetClaimValue("Id"));
                CultureType culture = CultureHelper.GetCulture(Request.HttpContext);


                var requestItem = _bOFetch.RequestItem(model, culture);

                var response = Mapper.Map<RequestItem, RequestItemDTO>(requestItem);
                Mapper.Map(requestItem.RequestItemML.FirstOrDefault(x => x.Culture == CultureHelper.Culture), response);


                if (requestItem != null)
                    return Ok(new CustomResponse<RequestItemDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = response });
                else
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Something went wrong! Try again." } });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetMyBookings")]
        public IActionResult GetMyBookings(int Items = 5, int Page = 0, int? Type = 0)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var User_Id = Convert.ToInt32(User.GetClaimValue("Id"));
                CultureType culture = CultureHelper.GetCulture(Request.HttpContext);

                PendingRequestViewModel pendingResponse = new PendingRequestViewModel();
                HistoryRequestViewModel historyResponse = new HistoryRequestViewModel();
                //MyBookingsDTO responseModel = new MyBookingsDTO();

                if (Type == 0)
                {
                    #region Get Pending Requests
                    var PendingRequestItems = _bOFetch.GetPendingRequests(User_Id, Items, Page, culture);

                    if (PendingRequestItems != null)
                    {
                        Mapper.Map(PendingRequestItems, pendingResponse.Pending);
                        for (int i = 0; i < PendingRequestItems.Count; i++)
                        {
                            Mapper.Map(PendingRequestItems[i].RequestItemML.FirstOrDefault(y => y.Culture == culture), pendingResponse.Pending[i]);
                        }
                    }
                    else
                        pendingResponse.Pending = new List<RequestItemDTO>();

                    #endregion
                    return Ok(new CustomResponse<PendingRequestViewModel> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = pendingResponse });
                }
                else
                {
                    #region Get History Of Requests
                    var DeliveredOrCompletedRequestItems = _bOFetch.GetDeliveredOrCompletedRequests(User_Id, Items, Page, culture);
                    if (DeliveredOrCompletedRequestItems != null)
                    {
                        Mapper.Map(DeliveredOrCompletedRequestItems, historyResponse.History);
                        for (int i = 0; i < DeliveredOrCompletedRequestItems.Count; i++)
                        {
                            Mapper.Map(DeliveredOrCompletedRequestItems[i].RequestItemML.FirstOrDefault(y => y.Culture == culture), historyResponse.History[i]);
                        }
                    }
                    else
                        historyResponse.History = new List<RequestItemDTO>();
                    #endregion
                    return Ok(new CustomResponse<HistoryRequestViewModel> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = historyResponse });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetRequestById")]
        public IActionResult GetRequestById(int Request_Id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                CultureType culture = CultureHelper.GetCulture(Request.HttpContext);

                RequestItemViewModel responseModel = new RequestItemViewModel();

                #region Get Pending Requests
                var SingleRequest = _bOFetch.GetRequestById(Request_Id, culture);
                responseModel.Request = Mapper.Map<RequestItem, RequestItemDTO>(SingleRequest);
                Mapper.Map(SingleRequest.RequestItemML.FirstOrDefault(x => x.Culture == culture), responseModel.Request);
                #endregion

                return Ok(new CustomResponse<RequestItemViewModel> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = responseModel });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }

        //[HttpPost]
        //[Route("GetMyBookings")]
        //public IActionResult GetMyBookings()
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //            return BadRequest(ModelState);

        //        var requestItem = _bOFetch.RequestItem(model);

        //        var response = Mapper.Map<RequestItem, RequestItemDTO>(requestItem);

        //        if (requestItem != null)
        //            return Ok(new CustomResponse<RequestItemDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = response });
        //        else
        //            return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Something went wrong! Try again." } });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(Error.LogError(ex));
        //    }
        //}

        [Route("GetDistance")]
        [HttpGet]
        public async Task<IActionResult> GetDistance()
        {
            var resp = _bOFetch.GetDistance();
            return Ok(new CustomResponse<string> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = "" });

        }

    }
}