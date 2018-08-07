using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Spatial;
using System.Text;

namespace DAL
{
    public class Store
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        //public double? Latitude { get; set; }

        //public double? Longitude { get; set; }

        public TimeSpan Open_From { get; set; }

        public TimeSpan Open_To { get; set; }

        [NotMapped]
        public double AverageRating { get; set; }

        [NotMapped]
        public double Distance { get; set; }

        public string ImageUrl { get; set; }

        public string Address { get; set; }

        public string ContactNumber { get; set; }

        public int AverageDeliveryTime { get; set; }

        public float? MinOrder { get; set; }

        public double DeliveryFee { get; set; }

        public string PaymentMethod { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsFeature { get; set; }

        public string About { get; set; }

        //[ComplexType]
        //public class Location
        //{
        //    public double Longitude { get; set; }

        //    public double Latitude { get; set; }
        //}
        public Location Location { get; set; }

        public virtual StoreTiming StoreTiming { get; set; }

        public virtual List<StoreRating> StoreRatings { get; set; }

        public virtual List<StoreCuisine> StoreCuisines { get; set; }

    }
}
