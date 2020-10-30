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
using Microsoft.Extensions.Logging;
using System.IO;

namespace Garage2._0MVC.Controllers
{
    public class VehicleModels2Controller : Controller
    {
        private readonly Garage2_0MVCContext db;
        private readonly IParkingService parkingService;
        private readonly IParkingCapacityService parkingCapacityService;

        public VehicleModels2Controller(Garage2_0MVCContext db, IParkingService parkingService,
            IParkingCapacityService parkingCapacityService)
        {
            this.db = db;
            this.parkingService = parkingService;
            this.parkingCapacityService = parkingCapacityService;
        }

        // GET: VehicleModels2
        public async Task<IActionResult> Index()
        {
            var vehicles = await GetVehicleList();

            var model = new VehicleIndexViewModel
            {
                Vehicles = vehicles,
            };

            return View(model);
        }

        public async Task<IActionResult> Search(string regSearch, VehicleIndexViewModel viewModel)
        {
            bool isRegSearch = !string.IsNullOrWhiteSpace(regSearch);
            bool isTypeSearch = viewModel.Type != null;

            var vehicles = await GetVehicleList();

            if (isRegSearch && isTypeSearch)
                vehicles = vehicles.Where(v => v.RegNum.Contains(regSearch) && v.Type == viewModel.Type).ToList();

            else if (isRegSearch)
                vehicles = vehicles.Where(v => v.RegNum.Contains(regSearch)).ToList();

            else if (isTypeSearch)
                vehicles = vehicles.Where(v => v.Type == viewModel.Type).ToList();

            var model = new VehicleIndexViewModel
            {
                Vehicles = vehicles,
            };

            return View(nameof(Index), model);
        }

        private async Task<List<VehicleListViewModel>> GetVehicleList()
        {
            return await db.VehicleModel.Include(v => v.Member).Include(v => v.VehicleType)
                .Select(v => new VehicleListViewModel
                {
                    Id = v.Id,
                    Type = v.Type,
                    RegNum = v.RegNum,
                    ArrivalTime = v.ArrivalTime,
                    Owner = v.Member.FullName
                }).ToListAsync();
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
                var capacity = (int)db.VehicleType.Find((int)viewmodel.Type).Capacity;          //Ger null reference exception för car
                //TODO round up for motorcycle capacity
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


                //int? maxParkingNum = db.ParkingSpace.Select(p => p.ParkingNum).Max();      

                int firstFree = GetFirstFreeParking(capacity);
                if (firstFree != 0)
                {
                    db.Add(vehicle);

                    for (int i = 0; i < capacity; i++)
                    {
                        //var freeParkingSpace = db.ParkingSpace.Where(p => p.ParkingNum == null).FirstOrDefault();
                        //freeParkingSpace.ParkingNum = freeParkingSpace.Id;

                        var parkingSpace = db.ParkingSpace.Where(p => p.Id == firstFree + i).FirstOrDefault();
                        parkingSpace.ParkingNum = firstFree + i;
                        var vehicleSpace = new VehicleModelParkingSpace();
                        vehicleSpace.VehicleModel = vehicle;
                        vehicleSpace.ParkingSpace = parkingSpace;
                        db.Add(vehicleSpace);

                        await db.SaveChangesAsync();

                    }

                }
                //Populates the parking according to vehicle capacity
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

        private int GetFirstFreeParking(int capacity)
        {
            var result = Enumerable.Range(1, 20).Except(db.ParkingSpace.Where(p => p.ParkingNum != null).Select(p => p.Id));
            if (capacity == 1)
            {
                return result.FirstOrDefault();
            }
            if (capacity == 2)
            {
                for (int i = 0; i < result.Count() - 2; i++)
                {
                    if (result.ElementAt(i + 1) - result.ElementAt(i) == 1 && result.ElementAt(i + 2) - result.ElementAt(i + 1) == 1)
                        return result.ElementAt(i);
                }
            }
            if (capacity == 3)
            {
                for (int i = 0; i < result.Count() - 3; i++)
                {
                    if (result.ElementAt(i + 1) - result.ElementAt(i) == 1 && result.ElementAt(i + 2) - result.ElementAt(i + 1) == 1
                        && result.ElementAt(i + 3) - result.ElementAt(i + 2) == 1)
                        return result.ElementAt(i);
                }
            }
            return 0;
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
