using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class ReportProblemMessage
    {
        public int Id { get; set; }

        public string Reason { get; set; }

        public bool IsDeleted { get; set; }
    }
}
