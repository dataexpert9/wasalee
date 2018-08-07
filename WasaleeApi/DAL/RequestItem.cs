using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class RequestItem
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

        public string PickUpLocation { get; set; }

        public string DropOffLocation { get; set; }

        public List<RequestItemImages> RequestItemImages { get; set; }

    }
}
