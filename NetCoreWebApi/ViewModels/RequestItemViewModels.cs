using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wasalee.DTOs;

namespace Wasalee.ViewModels
{
    public class RequestItemViewModels
    {
        


    }
    public class RequestItemViewModel
    {
        public RequestItemViewModel()
        {
            Request = new RequestItemDTO();
        }
        public RequestItemDTO Request { get; set; }
    }
   
}
