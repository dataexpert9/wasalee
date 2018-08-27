using System;
using System.Collections.Generic;
using System.Text;

namespace Component.Models.Driver
{
    public class RateDriverBindingModel
    {
        public int Request_Id { get; set; }

        public double Rating { get; set; }

        public string Reason { get; set; }

        public int Driver_Id { get; set; }

        public int User_Id { get; set; }

        //public int? ReportProblemMessage_Id { get; set; }

    }
    public class ReportProblemBindingModel
    {
        public int Request_Id { get; set; }

        public int User_Id { get; set; }

        public int Driver_Id { get; set; }

        public int? ReportProblemMessage_Id { get; set; }
    }
    public class CancelRequestBindingModel
    {

        public int RequestItem_Id { get; set; }

        public int? ReportProblemMessage_Id { get; set; }

    }
   public class FeedbackToDriverBindingModel
    {
        public int Id { get; set; }

        public int User_Id { get; set; }

        public int Driver_Id { get; set; }

        public string Feedback { get; set; }
    }
}
