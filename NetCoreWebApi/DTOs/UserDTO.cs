using Component.Utility;
using Component.Utility.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wasalee.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string Location { get; set; }

        public CultureType Culture { get; set; }

        public string ProfilePictureUrl { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string ZipCode { get; set; }

        public string DateofBirth { get; set; }

        public int? SignInType { get; set; }

        public short? Status { get; set; }

        public bool EmailConfirmed { get; set; }

        public bool PhoneConfirmed { get; set; }

        public bool IsNotificationsOn { get; set; }

        public string Token { get; set; }

        public string VerificationCode { get; set; }

        public SettingDTO Settings { get; set; }

    }

    public class SettingDTO
    {
        public int Id { get; set; }
        public string AboutUs { get; set; }
        public string PrivacyPolicy { get; set; }
        public string TermsOfUse { get; set; }
    }
}
