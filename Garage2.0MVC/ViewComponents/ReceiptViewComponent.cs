using Garage2._0MVC.Data;
using Garage2._0MVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2._0MVC.ViewComponents
{
    public class ReceiptViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var departure = DateTime.Now;

            var model = new ReceiptViewModel
            {
                DepartureTime = departure
            };

            return View(model);
        }
    }
}
