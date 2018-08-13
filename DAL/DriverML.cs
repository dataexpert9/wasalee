using Component.Utility.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class DriverML
    {
        public int Id { get; set; }

    

        public CultureType Culture { get; set; }

        public int Driver_Id { get; set; }
        [JsonIgnore]
        public virtual Driver Driver { get; set; }
    }
}
