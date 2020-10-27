using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2._0MVC.Models
{
    public class ParkingSpace
    {
        public int Id { get; set; }
        public int ParkingNum { get; set; }

        public ICollection<VehicleModelParkingSpace> VehicleModelParkingSpaces { get; set; }

    }
}
