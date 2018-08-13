using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL
{
    public class Driver : BaseModel
    {
        public int Id { get; set; }
        public string DateOfBirth { get; set; }
        public string PhoneNo { get; set; }
        public string LicenseNo { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsNotificationsOn { get; set; }
        public string Password { get; set; }
        public bool IsAvailable { get; set; }
        public int SignInType { get; set; }
        public string Email { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string FullName { get; set; }
        public string HomeAddress { get; set; }
        public string BriefInfo { get; set; }
        public string WorkHistory { get; set; }
        [NotMapped]
        public double AverageRating { get; set; }
        public virtual List<RequestItem> RequestItem { get; set; }
        [JsonIgnore]
        public virtual List<DriverRating> DriverRating { get; set; }


    }
}
