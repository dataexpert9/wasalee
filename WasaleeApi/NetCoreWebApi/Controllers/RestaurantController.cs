using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Interface;
using Component.ResponseFormats;
using Component.Utility;
using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Wasalee.DTOs;
using Wasalee.ViewModels;

namespace Wasalee.Controllers
{
    [Produces("application/json")]
    [Route("api/Restaurant")]
    public class RestaurantController : Controller
    {


        public IConfiguration _configuration { get; }
        protected readonly IBOStore _bOStore;
        protected readonly DataContext _dbContext;


        public RestaurantController(DataContext dataContext, IConfiguration configuration, IBOStore bOStore)
        {
            _dbContext = dataContext;
            _configuration = configuration;
            _bOStore = bOStore;
        }


        [Route("Home")]
        [HttpGet]
        public async Task<IActionResult> Home()
        {
            try
            {
                HomeViewModel returnModel = new HomeViewModel();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var cuisines = _bOStore.HomeCuisines();
                if (cuisines != null)
                {
                    returnModel.Cuisines = Mapper.Map<List<Cuisine>, List<CuisineDTO>>(cuisines);
                }
                var Restaurants = _bOStore.HomeRestaurants();
                if (Restaurants != null)
                {
                    returnModel.Stores = Mapper.Map<List<Store>, List<StoreDTO>>(Restaurants);
                }

                return Ok(new CustomResponse<HomeViewModel> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = returnModel });

            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }

    }
}