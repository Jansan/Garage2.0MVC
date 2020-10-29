using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2._0MVC.Models.ViewModels
{
    public class VehicleIndexViewModel
    {
        public int Id { get; set; }
        public IEnumerable<VehicleListViewModel> Vehicles { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }
        public VehicleTypeEnum? Type { get; set; }
        public string RegNum { get; set; }
    }
}
