using Component.ResponseFormats;
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


    }
}
