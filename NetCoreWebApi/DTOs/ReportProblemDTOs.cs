using Component.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wasalee.DTOs
{
    public class ReportProblemDTO
    {
        public int Id { get; set; }

        public string Reason { get; set; }

        public int Type { get; set; }

    }
    public class ReportProblemViewModel
    {
        public ReportProblemViewModel()
        {
            ReportProblems = new List<ReportProblemDTO>();
        }
        public List<ReportProblemDTO> ReportProblems { get; set; }
    }
}
