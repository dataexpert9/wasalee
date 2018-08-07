using Component.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class DriverML
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string HomeAddress { get; set; }

        public string BriefInfo { get; set; }

        public string WorkHistory { get; set; }

        public CultureType Culture { get; set; }

        public int Driver_Id { get; set; }

        public virtual Driver Driver { get; set; }
    }
}
