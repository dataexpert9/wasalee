using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Driver : BaseModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string DateOfBirth { get; set; }
        public string PhoneNo { get; set; }
        public string HomeAddress { get; set; }
        public string LicenseNo { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsNotificationsOn { get; set; }
        public string Password { get; set; }
        public int SignInType { get; set; }
        public string Email { get; set; }
    }
}
