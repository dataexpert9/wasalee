using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class UserDevice
    {
        public int Id { get; set; }
        public string DeviceName { get; set; }
        public string UDID { get; set; }
        public bool Platform { get; set; }
        public int? User_Id { get; set; }
        public string AuthToken { get; set; }
        public bool IsActive { get; set; }
        public User User { get; set; }
    }
}
