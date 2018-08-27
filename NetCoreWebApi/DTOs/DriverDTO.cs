using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wasalee.Utility;

namespace Wasalee.DTOs
{
    public class DriverDTO
    {
        public DriverDTO()
        {
            DriverRating = new List<DriverRatingDTO>();
            //FullName = FullName.Substring(0, 1).ToUpper() + FullName.Substring(1, FullName.Length).ToUpper();
        }
        public int Id { get; set; }
        public string FullName { get; set; }
        public string DateOfBirth { get; set; }
        public string PhoneNo { get; set; }
        public string HomeAddress { get; set; }
        public string LicenseNo { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsNotificationsOn { get; set; }
        public int SignInType { get; set; }
        public string Email { get; set; }
        public bool IsAvailable { get; set; }
        public string Token { get; set; }
        public string BriefInfo { get; set; }
        public string WorkHistory { get; set; }
        public string ProfilePictureUrl { get; set; }
        public double AverageRating { get; set; }
        public List<DriverRatingDTO> DriverRating { get; set; }
    }
    public class DriverRatingDTO
    {

        public DriverRatingDTO()
        {
            User = new UserDTO();
        }

        public int Id { get; set; }

        public double Rating { get; set; }

        public string Reason { get; set; }

        public int Driver_Id { get; set; }

        [JsonConverter(typeof(JsonCustomDateTimeConverter))]
        public DateTime CreatedDate { get; set; }

        [JsonConverter(typeof(JsonCustomDateTimeConverter))]
        public DateTime RatedAt { get; set; }

        public int? User_Id { get; set; }

        public int? ReportProblemMessage_Id { get; set; }

        public virtual ReportProblemMessageDTO ReportProblemMessage { get; set; }

        public UserDTO User { get; set; }

    }
    public class ReportProblemMessageDTO
    {
        public int Id { get; set; }

        public string Reason { get; set; }

        public int Type { get; set; }

    }
}
