using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Threading.Tasks;

namespace Garage2._0MVC.Models
{
    public class VehicleModelParkingSpace
    {
        public int VehicleModelId { get; set; }
        public int ParkingSpaceId { get; set; }

        public VehicleModel VehicleModel { get; set; }
        public ParkingSpace ParkingSpace { get; set; }
    }
}
