using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2._0MVC.Models.ViewModels
{
    public class VehicleListViewModel
    {
        public int Id { get; set; }

        [DisplayName("Vehicle Type")]
        public VehicleTypeEnum Type { get; set; }

        [DisplayName("Registration Number")]
        public string RegNum { get; set; }

        [DisplayName("Arrival Time")]
        public DateTime ArrivalTime { get; set; }

        public string Owner { get; set; }

        [DisplayName("Parking Number")]
        public int ParkingNumber { get; set; }

        //[DisplayName("Parking Spaces Left")]
        //public int ParkingSpacesLeft { get; set; }

        //[DisplayName("Total Spaces")]
        //public int TotalSpaces { get; set; }
    }
}
