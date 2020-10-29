using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2._0MVC.Models.ViewModels
{
    public class MemberViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        [DisplayName("Amount Vehicles")]
        public int AmountVehicles { get; set; }
    }
}
