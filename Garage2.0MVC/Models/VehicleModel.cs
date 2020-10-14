using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2._0MVC.Models
{
    public class VehicleModel
    {
        public int Id { get; set; }

        [Required]
        public VehicleType Type { get; set; }

        [Required]
        [StringLength(maximumLength: 6, MinimumLength = 6)]
        public string RegNum { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]  
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public int NumWheels { get; set; }

        public DateTime ArrivalTime { get; set; }
    }
}
