using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL
{
    public partial class DriverRating
    {
        public int Id { get; set; }

        public double Rating { get; set; }

        public string Reason { get; set; }

        public int Driver_Id { get; set; }

        public virtual Driver Driver { get; set; }

        [ForeignKey("ReportProblemMessage")]
        public int? ReportProblemMessage_Id { get; set; }

        public virtual ReportProblemMessage ReportProblemMessage { get; set; }

    }
}
