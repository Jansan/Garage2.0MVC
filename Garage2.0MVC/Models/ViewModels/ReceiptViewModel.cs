using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2._0MVC.Models.ViewModels
{
    public class ReceiptViewModel
    {
        [DisplayName("Departure Time")]
        public DateTime DepartureTime { get; set; }

        [DisplayName("Total Time")]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm\\:ss}", ApplyFormatInEditMode = true)]
        public TimeSpan TotalTime { get; set; }

        [DisplayName("Price (5:- / min)")]
        public double Price { get; set; }
    }
}
