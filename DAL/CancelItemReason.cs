using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL
{
    public class CancelItemReason
    {
        public int Id { get; set; }

        public DateTime CancelAt { get; set; }

        [ForeignKey("RequestItem")]
        public int RequestItem_Id { get; set; }

        public virtual RequestItem RequestItem { get; set; }

        [ForeignKey("ReportProblemMessage")]
        public int? ReportProblemMessage_Id { get; set; }

        public virtual ReportProblemMessage ReportProblemMessage { get; set; }


    }
}
