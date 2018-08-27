using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL
{
    public class User
    {
        public User()
        {
            UserDevices = new HashSet<UserDevice>();
            //Settings = new Settings();
        }

        public int Id { get; set; }

        //public string FirstName { get; set; }

        //public string LastName { get; set; }

        //public string FullName { get; set; }
        public string FullName { get; set; }

        public string Location { get; set; }

        public string ProfilePictureUrl { get; set; }

        public string Email { get; set; }

        public string PhoneNo { get; set; }


        [JsonIgnore]
        public string Password { get; set; }

        public string ZipCode { get; set; }

        public DateTime DateofBirth { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? SignInType { get; set; }

        public short? Status { get; set; }

        public bool EmailConfirmed { get; set; }

        public bool PhoneConfirmed { get; set; }

        public bool IsNotificationsOn { get; set; }

        [NotMapped]
        public Token Token { get; set; }
        
        public virtual ICollection<UserDevice> UserDevices { get; set; }

        public virtual ICollection<RequestItem> RequestItem { get; set; }

        public virtual ICollection<DriverRating> DriverRating { get; set; }

        //public string Location { get; set; }

        [NotMapped]
        public Settings Settings { get; set; }
    }
}
