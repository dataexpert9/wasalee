using Component.ResponseFormats;
using Component.Utility.Enums;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Utility
{
    public static class Extensions
    {

        public static void CalculateStoreAverageRating(this Store store)
        {
            try
            {
                foreach (var item in store.StoreRatings)
                {
                    store.AverageRating = store.StoreRatings.Average(x => x.Rating);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void CalculateDriverAverageRating(this Driver driver)
        {
            try
            {
                if (driver.DriverRating.Count > 0)
                    driver.AverageRating = Convert.ToDouble(driver.DriverRating.Where(x=>x.Type==(int)RatingTypes.RateDriver).Average(x => x.Rating).ToString("F2"));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public static void GetDriverAverageRating(this Driver driver)
        //{
        //    if (driver.DriverRating.Count > 0)
        //        driver.AverageRating = Convert.ToDouble(driver.DriverRating.Average(x => x.Rating).ToString("F2"));
        //}
    }
}
