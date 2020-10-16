using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2._0MVC.Models.ViewModels
{
    public class VehiclesCollectionViewModel
    {
        public IEnumerable<VehicleViewModel> Vehicles { get; set; }

        [DisplayName("Parking spaces left")]
        public int ParkingSpacesLeft { get; set; }
    }
}
