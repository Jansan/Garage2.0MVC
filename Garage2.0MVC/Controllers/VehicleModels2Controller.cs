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
using System.IO;

namespace Garage2._0MVC.Controllers
{
    public class VehicleModels2Controller : Controller
    {
        private readonly Garage2_0MVCContext db;
        private readonly IParkingService parkingService;
        private readonly IParkingCapacityService parkingCapacityService;
        private readonly ITypeSelectService typeSelectService;

        public VehicleModels2Controller(Garage2_0MVCContext db, IParkingService parkingService,
            IParkingCapacityService parkingCapacityService, ITypeSelectService typeSelectService)
        {
            this.db = db;
            this.parkingService = parkingService;
            this.parkingCapacityService = parkingCapacityService;
            this.typeSelectService = typeSelectService;
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
            var vehicleCapacity = parkingCapacityService.GetVehicleCapacity(); //TODO flytta till Post annars ta bort

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
                var capacity = (int)db.VehicleType.Find((int)viewmodel.Type).Capacity;

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
                    MemberId = viewmodel.MemberId,

                };


                //int? maxParkingNum = db.ParkingSpace.Select(p => p.ParkingNum).Max();       //TODO fixa så att klarar av en tom databas
                db.Add(vehicle);

                for (int i = 0; i < capacity; i++)
                {
                    var freeParkingSpace = db.ParkingSpace.Where(p => p.ParkingNum == null).FirstOrDefault();
                    freeParkingSpace.ParkingNum = freeParkingSpace.Id;

                    var vehicleSpace = new VehicleModelParkingSpace();
                    vehicleSpace.VehicleModel = vehicle;
                    vehicleSpace.ParkingSpace = freeParkingSpace;
                    db.Add(vehicleSpace);
                    await db.SaveChangesAsync();

                }
                //var vehicleSpace = vehicle.VehicleModelParkingSpaces.Where(v=>v.VehicleModelId == vehicle.Id)
















                //var maxParkingNum = db.ParkingSpace.Where(p => p.ParkingNum == null).Take(capacity);

                ////var firstNull = maxParkingNum[2];
                //var firstFreeParkingSpace = maxParkingNum.Select(v => v.Id).FirstOrDefault();


                //var first = Enumerable.Range(1, 20).Except(db.ParkingSpace.Where(p => p.ParkingNum != null).Select(p => p.Id)).FirstOrDefault();
                //int[] comparer = new int[capacity];
                //var result = Enumerable.Range(1, 20).Except(db.ParkingSpace.Where(p => p.ParkingNum != null).Select(p => p.Id)).SequenceEqual(comparer);

                ////TODO tomma parkeringsnummer ska fyllas på först
                //for (int i = 0; i < (int)capacity; i++)
                //{
                //    var parkingSpace = new ParkingSpace();
                //    //if (maxParkingNum is null)
                //    //{
                //    //    maxParkingNum = 0;
                //    //}
                //    db.ParkingSpace.Where(p => p.Id == first).Select(p => p.ParkingNum)

                //    parkingSpace.ParkingNum = firstFreeParkingSpace + i;
                //    db.Add(parkingSpace);
                //    var vehicleModelParkingSpace = new VehicleModelParkingSpace();
                //    vehicleModelParkingSpace.VehicleModel = vehicle;
                //    vehicleModelParkingSpace.ParkingSpace = parkingSpace;
                //    db.Add(vehicleModelParkingSpace);
                //}




                db.VehicleModel.Include(v => v.VehicleModelParkingSpaces).ThenInclude(v => v.ParkingSpace);
                var space = parkingService.GetCurrentParking();
                var vehicleCapacity = parkingCapacityService.GetVehicleCapacity();



                //db.Add(vehicle);
                //await db.SaveChangesAsync();
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

        // GET: VehicleModels2/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: VehicleModels2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicleModel = await db.VehicleModel.FindAsync(id);
            db.VehicleModel.Remove(vehicleModel);

            //await db.VehicleModelParkingSpaces.Include(v => v.ParkingSpace).FirstOrDefaultAsync(v => v.VehicleModelId == id);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleModelExists(int id)
        {
            return db.VehicleModel.Any(e => e.Id == id);
        }
    }
}
