using Newtonsoft.Json;
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

        public DateTime RatedAt { get; set; }

        public int Type { get; set; }// 0 = Report Driver, 1= Rate Driver 

        [JsonIgnore]
        public virtual Driver Driver { get; set; }

        public int? User_Id { get; set; }

        public virtual User User { get; set; }

        [ForeignKey("ReportProblemMessage")]
        public int? ReportProblemMessage_Id { get; set; }

        public virtual ReportProblemMessage ReportProblemMessage { get; set; }

    }
}
