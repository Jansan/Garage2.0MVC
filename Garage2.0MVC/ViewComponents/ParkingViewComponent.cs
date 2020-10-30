using Garage2._0MVC.Data;
using Garage2._0MVC.Models.ViewModels;
using Garage2._0MVC.Services;
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
        private const int totalParkingSpaces = 20;

        public ParkingViewComponent(Garage2_0MVCContext db)
        {
            this.db = db;
        }

        // Calculates parking space left & total parking ppaces
        // Made ParkingViewModel for display in index
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var total = totalParkingSpaces;
            var vehicles = await db.VehicleModel.Include(v => v.VehicleType).Select(v => v.VehicleType.Capacity).ToListAsync();
            int sum = 0;

            foreach (var item in vehicles)
            {
                sum += item; 
            }

            var space = totalParkingSpaces - sum;

            var model = new ParkingViewModel
            {
                ParkingSpacesLeft = space,
                TotalParkingSpaces = total
            };

            return View(model);
        }
    }
}
