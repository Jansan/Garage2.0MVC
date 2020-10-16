using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Garage2._0MVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Garage2._0MVC.Models.ViewModels;

namespace Garage2._0MVC.ViewComponents
{
    public class StatisticsViewComponent : ViewComponent
    {
        private readonly Garage2_0MVCContext db;

        public StatisticsViewComponent(Garage2_0MVCContext db)
        {
            this.db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync() 
        {
            var totalWheels = await db.VehicleModel.Select(v => v.NumWheels).SumAsync();

            var arrivalTimes = await db.VehicleModel.Select(v => v.ArrivalTime).ToListAsync();
            var departureTime = DateTime.Now;
            TimeSpan totalTime = TimeSpan.Zero;

            foreach (var arrivalTime in arrivalTimes)
            {
                totalTime += departureTime - arrivalTime;
            }

            var totalPrice = Math.Round(totalTime.TotalMinutes * 5);
            
            var model = new StatisticsViewModel
            {
                TotalWheels = totalWheels,
                TotalEarnings = totalPrice
            };
            
            return View(model);
        }
    }
}
