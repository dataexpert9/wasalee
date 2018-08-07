using BLL.Interface;
using Component.Utility;
using DAL;
using System;
using System.Collections.Generic;
using System.Text;
using Wasalee.BindingModels.FetchAnything;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BLL.Implementation
{
    public class BOFetch : IBOFetch
    {
        public DataContext _dbContext { get; set; }

        public BOFetch(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public RequestItem RequestItem(RequestItemBindingModel model)
        {

            var RequestItem = new RequestItem
            {
                Name = model.Name,
                DeliveryDate = model.DeliveryDate,
                DeliveryTime = model.DeliveryTime,
                Description = model.Description,
                IsDeleted = false,
                PaymentMethod = model.PaymentMethod,
                PriceRangeFrom = model.PriceRangeFrom,
                PriceRangeTo = model.PriceRangeTo,
                Quantity = model.Quantity,
                Status = (Int16)RequestItemStatus.Requested,
                DropOffLocation=model.DropOffLocation,
                PickUpLocation=model.PickUpLocation
            };

            _dbContext.RequestItem.Add(RequestItem);
            _dbContext.SaveChanges();


            foreach (var item in model.ItemImages)
            {
                _dbContext.RequestItemImages.Add(new RequestItemImages {

                    ImageUrl=item,
                    RequestItem_Id=RequestItem.Id
                });

            }

            _dbContext.SaveChanges();
            return _dbContext.RequestItem.Include(x=>x.RequestItemImages).FirstOrDefault(x=>x.Id==RequestItem.Id);
        }

        //public bool SaveRequestImages(RequestItemBindingModel model, int RequestItem_Id)
        //{
        //    if (model.ItemImages != null)
        //    {
        //        foreach (var image in model.ItemImages)
        //        {
        //            _dbContext.RequestItemImages.AddRangeAsync(new RequestItemImages
        //            {
        //                ImageUrl = image.SaveImage("RequestImages"),
        //                RequestItem_Id = RequestItem_Id
        //            });
        //            _dbContext.SaveChanges();
        //        }
        //    }
        //    return true;
        //}

        public double GetDistance()
        {
            Location loc = new Location(_dbContext);
            var store = _dbContext.Store.FirstOrDefault();
            return loc.Distance(store.Location);
        }

    }
}
