using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wasalee.DTOs;

namespace Wasalee.ViewModels
{
    public class HomeViewModel
    {
        public List<CuisineDTO> Cuisines { get; set; }
        public List<StoreDTO> Stores { get; set; }

    }
}
