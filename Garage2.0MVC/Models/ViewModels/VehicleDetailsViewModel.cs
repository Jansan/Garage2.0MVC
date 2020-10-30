using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2._0MVC.Models.ViewModels
{
    public class VehicleDetailsViewModel
    {
        public int Id { get; set; }
        public VehicleTypeEnum Type { get; set; }
        [DisplayName("Registration Number")]
        public string RegNum { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        [DisplayName("Owner")]
        public string Owner { get; set; }

        [DisplayName("Number of Wheels")]
        public int NumberWeels { get; set; }
        [DisplayName("Arrival Time")]
        public DateTime ArrivalTime { get; set; }

    }
}
