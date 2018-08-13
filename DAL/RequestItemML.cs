using Component.Utility.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class RequestItemML
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string Name { get; set; }

        public string ItemDescription { get; set; }

        public string PickUpLocation { get; set; }

        public string DropOffLocation { get; set; }

        public CultureType Culture { get; set; }

        public int RequestItem_Id { get; set; }

        [JsonIgnore]
        public virtual RequestItem RequestItem { get; set; }
    }
}
