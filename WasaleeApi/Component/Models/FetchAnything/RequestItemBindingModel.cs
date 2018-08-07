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

        public string Description { get; set; }

        public DateTime DeliveryDate { get; set; }

        public TimeSpan DeliveryTime { get; set; }

        public int PaymentMethod { get; set; }

        public string PickUpLocation { get; set; }

        public string DropOffLocation { get; set; }

        public List<string> ItemImages { get; set; }

        //public List<IFormFile> ItemImages { get; set; }
    }
}
