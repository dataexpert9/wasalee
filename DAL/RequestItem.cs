using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL
{
    public class RequestItem
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public double PriceRangeFrom { get; set; }

        public string ItemDescription { get; set; }

        public double PriceRangeTo { get; set; }

        public DateTime DeliveryDate { get; set; }

        public TimeSpan DeliveryTime { get; set; }

        public int PaymentMethod { get; set; }

        public short Status { get; set; }

        public bool IsDeleted { get; set; }

        public double? Price { get; set; }

        public double? PickUpLatitude { get; set; }

        public double? PickUpLongitude { get; set; }
        
        public double? DropOffLatitude { get; set; }

        public double? DropOffLongitude { get; set; }

        public bool IsUserRated { get; set; }

        public int User_Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        public virtual  User User { get; set; }

        public List<RequestItemImages> RequestItemImages { get; set; }

        public int? Driver_Id { get; set; }

        public virtual Driver Driver { get; set; }

        public virtual List<RequestItemML> RequestItemML { get; set; }

        [ForeignKey("DriverRating")]
        public int? DriverRating_Id { get; set; }

        public virtual DriverRating DriverRating { get; set; }


    }
}
