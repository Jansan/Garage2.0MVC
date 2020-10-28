using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2._0MVC.Models.ViewModels
{
    public class ParkingViewModel
    {
        [DisplayName("Parking Spaces Left")]
        public int ParkingSpacesLeft { get; set; }

        [DisplayName("Total Parking Spaces")]
        public int TotalParkingSpaces { get; set; }
    }
}
