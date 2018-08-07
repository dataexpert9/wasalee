using BLL.Interface;
using Component.Utility;
using DAL;
using System;
using System.Collections.Generic;
using System.Text;
using Wasalee.BindingModels.FetchAnything;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Component.Culture;
using Component.Utility.Enums;

namespace BLL.Implementation
{
    public class BOFetch : IBOFetch
    {
        public DataContext _dbContext { get; set; }

        public BOFetch(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public RequestItem RequestItem(RequestItemBindingModel model,CultureType culture)
        {

            var RequestItem = new RequestItem
            {
                DeliveryDate = model.DeliveryDate,
                DeliveryTime = model.DeliveryTime,
                IsDeleted = false,
                PaymentMethod = model.PaymentMethod,
                PriceRangeFrom = model.PriceRangeFrom,
                PriceRangeTo = model.PriceRangeTo,
                Quantity = model.Quantity,
                Status = (Int16)RequestItemStatus.Pending,
                User_Id=model.User_Id,
                PickUpLatitude=model.PickUpLatitude,
                PickUpLongitude=model.PickUpLongitude
            };

            if (RequestItem.RequestItemML == null)
                RequestItem.RequestItemML = new List<RequestItemML>();

            RequestItem.RequestItemML.Add(new RequestItemML {
                Name = model.Name,
                Description = model.Description,
                DropOffLocation = model.DropOffLocation,
                PickUpLocation = model.PickUpLocation,
                Culture=culture
            });

            //_dbContext.SaveChanges();
            if (RequestItem.RequestItemImages == null)
                RequestItem.RequestItemImages = new List<RequestItemImages>();

            foreach (var item in model.ItemImages)
            {
                RequestItem.RequestItemImages.Add(new RequestItemImages {
                    ImageUrl= item
                });
                //_dbContext.RequestItemImages.Add(new RequestItemImages {
                //    ImageUrl=item,
                //    RequestItem_Id=RequestItem.Id
                //});
            }

            _dbContext.RequestItem.Add(RequestItem);
            _dbContext.SaveChanges();

            return RequestItem;
            //return _dbContext.RequestItem.Include(x => x.RequestItemML).Include(x => x.RequestItemImages).FirstOrDefault(x => x.Id == RequestItem.Id);
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

        public List<RequestItem> GetPendingRequests(int User_Id ,int Items,int Page,CultureType culture)
        {
            return _dbContext.RequestItem.Include(x=>x.RequestItemML).Include(x=>x.RequestItemImages).Where(x=>x.Status==(int)RequestItemStatus.Pending && x.User_Id==User_Id && x.IsDeleted==false).Skip(Items*Page).Take(Items).OrderBy(x=>x.Id).ToList();
        }

        public List<RequestItem> GetDeliveredOrCompletedRequests(int User_Id, int Items, int Page, CultureType culture)
        {
            return _dbContext.RequestItem.Include(x => x.RequestItemML).Include(x => x.RequestItemImages).Where(x => (x.Status == (int)RequestItemStatus.Delivered || x.Status == (int)RequestItemStatus.Completed) && x.User_Id == User_Id && x.IsDeleted==false).Skip(Items * Page).Take(Items).OrderBy(x => x.Id).ToList();
        }

        public RequestItem GetRequestById(int Request_Id, CultureType culture)
        {
            return _dbContext.RequestItem.Include(x => x.RequestItemML).Include(x=>x.RequestItemImages).Include(x => x.Driver).Include(x => x.User).FirstOrDefault(x => x.Id == Request_Id);
        }

    }
}
