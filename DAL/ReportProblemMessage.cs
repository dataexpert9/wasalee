using Component.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class ReportProblemMessage
    {
        public int Id { get; set; }

        public string Reason { get; set; }

        public CultureType Culture { get; set; }

        public int Type { get; set; }

        public bool IsDeleted { get; set; }
    }
}
