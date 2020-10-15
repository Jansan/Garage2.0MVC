using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2._0MVC.Models.ViewModels
{
    public class ReceiptViewModel
    {
        [DisplayName("Departure Time")]
        public DateTime DepartureTime { get; set; }

        [DisplayName("Total Time")]
        public TimeSpan TotalTime { get; set; }

        public double Price { get; set; }
    }
}
