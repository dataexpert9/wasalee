using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Wasalee.BindingModels.FetchAnything
{
    public class RequestItemBindingModel
    {
        public string Name { get; set; }

        public int Quantity { get; set; }

        public double PriceRangeFrom { get; set; }

        public double PriceRangeTo  { get; set; }

        public string ItemDescription { get; set; }

        public DateTime DeliveryDate { get; set; }

        public TimeSpan DeliveryTime { get; set; }

        public int PaymentMethod { get; set; }

        public string PickUpLocation { get; set; }

        public double? PickUpLatitude { get; set; }

        public double? PickUpLongitude { get; set; }

        public string DropOffLocation { get; set; }

        public double? DropOffLatitude { get; set; }

        public double? DropOffLongitude { get; set; }

        public List<string> ItemImages { get; set; }

        public int User_Id { get; set; }

    }
}
