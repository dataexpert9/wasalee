using System;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Interface;
using Component.ResponseFormats;
using Component.Utility;
using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Wasalee.BindingModels.FetchAnything;
using Wasalee.DTOs;

namespace Wasalee.Controllers
{
    [Produces("application/json")]
    [Route("api/FetchAnything")]
    public class FetchAnythingController : Controller
    {
        public IConfiguration _configuration { get; }
        protected readonly IBOFetch _bOFetch;
        protected readonly DataContext _dbContext;


        public FetchAnythingController(DataContext dataContext,IConfiguration configuration, IBOFetch bOFetch)
        {
            _dbContext = dataContext;
            _configuration = configuration;
            _bOFetch = bOFetch;
        }

        [HttpPost]  
        [Route("RequestItem")]
        public IActionResult RequestItem(RequestItemBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var requestItem = _bOFetch.RequestItem(model);

                var response = Mapper.Map<RequestItem, RequestItemDTO>(requestItem);

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

        [Route("GetDistance")]
        [HttpGet]
        public async Task<IActionResult> GetDistance()
        {
            var resp = _bOFetch.GetDistance();
            return Ok(new CustomResponse<string> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = ""});
            
        }
    }
}