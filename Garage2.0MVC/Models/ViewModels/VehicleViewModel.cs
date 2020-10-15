using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2._0MVC.Models.ViewModels
{
    public class VehicleViewModel
    {
       
        public VehicleType VehicleType { get; set; }
        public string RegNum { get; set; }
        public DateTime ArrivalTime { get; set; }

    }
}
