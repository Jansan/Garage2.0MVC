using Garage2._0MVC.Data;
using Garage2._0MVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2._0MVC.ViewComponents
{
    public class ParkingViewComponent : ViewComponent
    {
        private readonly Garage2_0MVCContext db;

        // Sets Total Parking Space in this class
        private const int totalParkingSpaces = 5;

        public ParkingViewComponent(Garage2_0MVCContext db)
        {
            this.db = db;
        }

        // Calculates parking space left & total parking ppaces
        // Made ParkingViewModel for display in index
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var total = totalParkingSpaces;
            var vehicles = await db.VehicleModel.CountAsync();
            var space = totalParkingSpaces - vehicles;

            var model = new ParkingViewModel
            {
                ParkingSpacesLeft = space,
                TotalParkingSpaces = total
            };

            return View(model);
        }
    }
}
