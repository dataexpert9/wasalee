using Component.Utility;
using Component.Utility.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class UserML
    {
        [JsonIgnore]
        public int Id { get; set; }

      

        public CultureType Culture { get; set; }

        public int User_Id { get; set; }

        public virtual User User { get; set; }
    }
}
