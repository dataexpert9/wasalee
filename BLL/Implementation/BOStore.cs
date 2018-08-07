using BLL.Interface;
using DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BLL.Utility;

namespace BLL.Implementation
{
   public class BOStore : IBOStore
    {
        public DataContext _dbContext { get; set; }


        public BOStore(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Cuisine> HomeCuisines()
        {
            return _dbContext.Cuisine.Where(x => x.IsDeleted == false).ToList();
        }
        
        public List<Store> HomeRestaurants()
        {
            var stores = _dbContext.Store.Include(x=>x.StoreRatings).Where(x =>x.IsFeature==true && x.IsDeleted == false).ToList();
            
            if(stores != null)
            {
                foreach (var store in stores)
                {
                    store.CalculateStoreAverageRating();
                }
            }
            return stores;
        }
    }
}
