using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wasalee.DTOs
{
    public class StoreDTO
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public TimeSpan Open_From { get; set; }

        public TimeSpan Open_To { get; set; }

        public double AverageRating { get; set; }

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
    }
    
}
