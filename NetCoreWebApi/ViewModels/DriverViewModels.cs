using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wasalee.DTOs;

namespace Wasalee.ViewModels
{
    public class DriverViewModels
    {
    }
    public class DriverRatingViewModel
    {
        public DriverRatingViewModel()
        {
            Rating = new DriverRatingDTO();
        }
        public DriverRatingDTO Rating { get; set; }
    }
    public class DriverDetailsViewModel
    {
        public DriverDetailsViewModel()
        {
            Driver = new DriverDTO();
        }
        public DriverDTO Driver { get; set; }
    }
    public class DriversRatingsViewModel
    {
        public DriversRatingsViewModel()
        {
            Ratings = new List<DriverRatingDTO>();
        }

        public List<DriverRatingDTO> Ratings { get; set; }

        public int TotalRecords { get; set; }
    }

}
