using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2._0MVC.Models.ViewModels
{
    public class StatisticsViewModel
    {
        public VehicleTypeEnum Type { get; set; }        //TODO Lägg till validering

        public int Count { get; set; }

        [DisplayName("Total wheels")]
        public int TotalWheels { get; set; }

        [DisplayName("Total earnings")]
        public double TotalEarnings { get; set; }
    }
}
