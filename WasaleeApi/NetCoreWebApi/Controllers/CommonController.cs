using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interface;
using Component.Models.Common;
using Component.ResponseFormats;
using Component.Utility;
using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

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

                if (resp.Count > 0)
                    return Ok(new CustomResponse<UploadImagesContainer> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = new UploadImagesContainer { Images = resp } });
                else
                    return Ok(new CustomResponse<Error> { Message = Global.ResponseMessages.Conflict, StatusCode = StatusCodes.Status409Conflict, Result = new Error { ErrorMessage = "Something went wrong! Try again." } });
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }

    }
}