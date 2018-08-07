using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wasalee.DTOs
{
    public class MyBookingsDTO
    {
        public MyBookingsDTO()
        {
            Pending = new List<RequestItemDTO>();
            History = new List<RequestItemDTO>();
        }
        public List<RequestItemDTO> Pending { get; set; }
        public List<RequestItemDTO> History { get; set; }

    }

    public class PendingRequestViewModel
    {
        public PendingRequestViewModel()
        {
            Pending = new List<RequestItemDTO>();
        }
        public List<RequestItemDTO> Pending { get; set; }
    }
    public class HistoryRequestViewModel
    {
        public HistoryRequestViewModel()
        {
            History = new List<RequestItemDTO>();
        }
        public List<RequestItemDTO> History { get; set; }
    }


    public class RiderMyRequests
    {
        public RiderMyRequests()
        {
            Pending = new List<RequestItemDTO>();
            History = new List<RequestItemDTO>();
        }
        public List<RequestItemDTO> Pending { get; set; }
        public List<RequestItemDTO> History { get; set; }

    }


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

        public double? PickUpLatitude { get; set; }

        public double? PickUpLongitude { get; set; }

        public int User_Id { get; set; }
        
        public List<RequestItemImagesDTO> RequestItemImages { get; set; }

        public UserDTO User { get; set; }

        public DriverDTO Driver { get; set; }
    }
    public class RequestItemImagesDTO
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public int RequestItem_Id { get; set; }
    }

}
