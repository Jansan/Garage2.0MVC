using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2._0MVC.Models
{
    public class VehicleModel
    {
        public int Id { get; set; }
        public Type Type { get; set; }
        public string RegNum { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int NumWheels { get; set; }
        public DateTime ArrivalTime { get; set; }
    }
}
