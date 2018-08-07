using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wasalee.DTOs
{
    public class RequestItemDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public double PriceRangeFrom { get; set; }

        public double PriceRangeTo { get; set; }

        public string Description { get; set; }

        public DateTime DeliveryDate { get; set; }

        public TimeSpan DeliveryTime { get; set; }

        public int PaymentMethod { get; set; }

        public short Status { get; set; }

        public bool IsDeleted { get; set; }
        
        public List<RequestItemImagesDTO> RequestItemImages { get; set; }
    }
    public class RequestItemImagesDTO
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public int RequestItem_Id { get; set; }
    }
}
