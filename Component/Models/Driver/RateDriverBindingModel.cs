using System;
using System.Collections.Generic;
using System.Text;

namespace Component.Models.Driver
{
    public class RateDriverBindingModel
    {
        public double Rating { get; set; }

        public string Reason { get; set; }

        public int Driver_Id { get; set; }

        public int? ReportProblemMessage_Id { get; set; }

    }
}
