using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wasalee.BindingModels.Requests
{
    public class RequestBindingModels
    {
    }
    public class CancelRequestBindingModel
    {
        public int Request_Id { get; set; }

        public int User_Id { get; set; }

        public int Driver_Id { get; set; }

        public int? ReportProblemMessage_Id { get; set; }

    }
}
