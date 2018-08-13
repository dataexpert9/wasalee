using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wasalee.Utility
{
    public class JsonCustomDateTimeConverter : IsoDateTimeConverter
    {
        public JsonCustomDateTimeConverter()
        {
            DateTimeFormat = "yyyy-MM-ddThh:mm";
        }
    }
}
