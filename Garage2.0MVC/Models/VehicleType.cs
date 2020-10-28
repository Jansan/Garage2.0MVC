using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2._0MVC.Models
{
    public class VehicleType
    {
        public int Id { get; set; }
        public VehicleTypeEnum Type { get ; set; }
        public double Capacity { get; set; }
    }
}
