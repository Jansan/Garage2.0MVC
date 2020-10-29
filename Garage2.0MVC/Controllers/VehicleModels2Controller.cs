using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage2._0MVC.Data;
using Garage2._0MVC.Models;
using Garage2._0MVC.Models.ViewModels;
using Garage2._0MVC.Services;
using jsreport.AspNetCore;
using jsreport.Types;

namespace Garage2._0MVC.Controllers
{
    public class VehicleModels2Controller : Controller
    {
        private readonly Garage2_0MVCContext db;
        private readonly IParkingService parkingService;

        public VehicleModels2Controller(Garage2_0MVCContext db, IParkingService parkingService)
        {
            this.db = db;
            this.parkingService = parkingService;
        }

        // GET: VehicleModels2
        public async Task<IActionResult> Index()
        {
            var model = await db.VehicleModel.Include(v => v.Member).Include(v => v.VehicleType)
                .Include(v => v.VehicleModelParkingSpaces).ThenInclude(v => v.ParkingSpace)
                .Select(l => new Vehicle2ListViewModel
                {
                    Id = l.Id,
                    Type = l.VehicleType.Type,
                    RegNum = l.RegNum,
                    ArrivalTime = l.ArrivalTime,
                    Owner = l.Member.FullName,
                    //ParkingNumber = l.VehicleModelParkingSpaces
                }).ToListAsync();

            return View(model);
        }

        // GET: VehicleModels2/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleModel = await db.VehicleModel
                .Include(v => v.Member)
                .Include(v => v.VehicleType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleModel == null)
            {
                return NotFound();
            }

            return View(vehicleModel);
        }

        // GET: VehicleModels2/Create
        public IActionResult Create()
        {
            var space = parkingService.GetCurrentParking();
            
           
            //ViewBag.isSuccess = isSuccess;
            //ViewBag.regNum = regNum;
            return View();
        }

        // POST: VehicleModels2/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,RegNum,Color,Brand,Model,NumWheels,ArrivalTime,VehicleTypeId,MemberId")] Vehicle2CreateViewModel viewmodel)
        {
            VehicleModel vehicle;
            if (ModelState.IsValid)
            {
                var capacity = db.VehicleType.Find((int)viewmodel.Type).Capacity;       //Hur göra databasuppslagning i property

                //var parkingviewmodel = new ParkingViewModel();
                //var pLeft = 2;//parkingviewmodel.ParkingSpacesLeft
                vehicle = new VehicleModel
                {
                    ArrivalTime = DateTime.Now,
                    Brand = viewmodel.Brand,
                    Color = viewmodel.Color,
                    Model = viewmodel.Model,
                    NumWheels = viewmodel.NumWheels,
                    RegNum = viewmodel.RegNum.ToUpper(),
                    Type = viewmodel.Type,
                    VehicleTypeId = (int)viewmodel.Type,
                    MemberId = viewmodel.MemberId
                   
                    //VehicleModelParkingSpaces = new ParkingSpace
                    //{
                    //    ParkingNum = 1
                    //}
                };
                for (int i = 0; i < capacity; i++)
                {
                    
                }


                db.Add(vehicle);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            //ViewData["MemberId"] = new SelectList(db.Member, "Id", "Id",vehicle.MemberId);
            //ViewData["VehicleTypeId"] = new SelectList(db.VehicleType, "Id", "Id", vehicle.VehicleTypeId);
            return View(viewmodel);
        }

        // GET: VehicleModels2/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleModel = await db.VehicleModel.FindAsync(id);
            if (vehicleModel == null)
            {
                return NotFound();
            }
            ViewData["MemberId"] = new SelectList(db.Member, "Id", "Id", vehicleModel.MemberId);
            ViewData["VehicleTypeId"] = new SelectList(db.VehicleType, "Id", "Id", vehicleModel.VehicleTypeId);
            return View(vehicleModel);
        }

        // POST: VehicleModels2/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,RegNum,Color,Brand,Model,NumWheels,ArrivalTime,VehicleTypeId,MemberId")] VehicleModel vehicleModel)
        {
            if (id != vehicleModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(vehicleModel);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleModelExists(vehicleModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MemberId"] = new SelectList(db.Member, "Id", "Id", vehicleModel.MemberId);
            ViewData["VehicleTypeId"] = new SelectList(db.VehicleType, "Id", "Id", vehicleModel.VehicleTypeId);
            return View(vehicleModel);
        }

        // GET: VehicleModels2/Unpark/5
        public async Task<IActionResult> Unpark(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleModel = await db.VehicleModel
                .Include(v => v.Member)
                .Include(v => v.VehicleType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleModel == null)
            {
                return NotFound();
            }

            return View(vehicleModel);
        }

        // POST: VehicleModels2/Unpark/5
        [HttpPost, ActionName("Unpark")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnparkConfirmed(int id)
        {
            var vehicleModel = await db.VehicleModel.FindAsync(id);
            db.VehicleModel.Remove(vehicleModel);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // GET: Receipt
        public async Task<IActionResult> Receipt(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vehicleModel = await db.VehicleModel
                .Include(m => m.Member)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (vehicleModel == null)
            {
                return NotFound();
            }

            return View(vehicleModel);
        }
        // GET: Print receipt
        [MiddlewareFilter(typeof(JsReportPipeline))]
        public async Task<IActionResult> Print(int id)
        {
            var vehicleModel = await db.VehicleModel
                .Include(m => m.Member)
                .FirstOrDefaultAsync(m => m.Id == id);
               

            HttpContext.JsReportFeature().Recipe(Recipe.ChromePdf);
            return View(vehicleModel);
        }

        private bool VehicleModelExists(int id)
        {
            return db.VehicleModel.Any(e => e.Id == id);
        }
    }
}
