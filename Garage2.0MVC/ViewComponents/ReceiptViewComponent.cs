using Garage2._0MVC.Data;
using Garage2._0MVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2._0MVC.ViewComponents
{
    public class ReceiptViewComponent : ViewComponent
    {
        private readonly Garage2_0MVCContext db;

        public ReceiptViewComponent(Garage2_0MVCContext db)
        {
            this.db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync(int vehicleId)
        {
            var vehicle = await db.VehicleModel.Where(v => v.Id == vehicleId).FirstOrDefaultAsync();

            var departure = DateTime.Now;
            var total = departure - vehicle.ArrivalTime;
            var price = Math.Round(total.TotalMinutes * 5);

            var model = new ReceiptViewModel
            {
                DepartureTime = departure,
                TotalTime = total,
                Price = price
            };

            return View(model);
        }
    }
}
