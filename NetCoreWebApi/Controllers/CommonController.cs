using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Interface;
using Component.Culture;
using Component.Models.Common;
using Component.Models.Driver;
using Component.ResponseFormats;
using Component.Utility;
using Component.Utility.Enums;
using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Wasalee.DTOs;

namespace Wasalee.Controllers
{
    [Produces("application/json")]
    [Route("api/Common")]
    public class CommonController : Controller
    {
        public IConfiguration _configuration { get; }
        protected readonly IBOCommon _bOCommon;
        protected readonly DataContext _dbContext;


        public CommonController(DataContext dataContext, IConfiguration configuration, IBOCommon bOCommon)
        {
            _dbContext = dataContext;
            _configuration = configuration;
            _bOCommon = bOCommon;
        }

        [HttpPost]
        [Route("UploadImages")]
        public IActionResult UploadImages(UploadImagesBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var resp = _bOCommon.UploadImages(model);
                resp.Type = Convert.ToInt32(model.Type);
                resp.TypeText = model.Type.ToString();

                if (resp !=null)
                    return Ok(new CustomResponse<UploadImagesContainer> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new UploadImagesContainer { Image = resp } });
                else
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Something went wrong! Try again." } });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }

        [Route("GetReportProblems")]
        [HttpGet]
        public async Task<IActionResult> GetReportProblems(int Type)
        {
            CultureType culture = CultureHelper.GetCulture(Request.HttpContext);

            ReportProblemViewModel response= new ReportProblemViewModel();
            var problems = _bOCommon.GetReportProblems(Type,culture);

            if (problems != null)
            {
                Mapper.Map(problems, response.ReportProblems);
            }
            else
                response.ReportProblems = new List<ReportProblemDTO>();

            return Ok(new CustomResponse<ReportProblemViewModel> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = response });
        }


        [HttpPost]
        [Route("CancelRequest")]
        public IActionResult CancelRequest(CancelRequestBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var resp = _bOCommon.CancelRequest(model);
                if (resp)
                    return Ok(new CustomResponse<string> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = "Request has been canelled." });
                else
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Invalid request id." } });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }

    }
}