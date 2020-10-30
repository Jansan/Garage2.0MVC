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
using Microsoft.Extensions.Logging;
using System.IO;

namespace Garage2._0MVC.Controllers
{
    public class VehicleModels2Controller : Controller
    {
        private readonly Garage2_0MVCContext db;
        private readonly IParkingService parkingService;
        private readonly IMemberSelectService memberSelectService;
        private readonly IParkingCapacityService parkingCapacityService;

        public VehicleModels2Controller(Garage2_0MVCContext db, IParkingService parkingService,
            IParkingCapacityService parkingCapacityService, IMemberSelectService memberSelectService)
        {
            this.db = db;
            this.parkingService = parkingService;
            this.memberSelectService = memberSelectService;
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
            return await db.VehicleModel.Include(v => v.Member).Include(v => v.VehicleType).Include(v => v.VehicleModelParkingSpaces).ThenInclude(p => p.ParkingSpace)
                .Select(v => new VehicleListViewModel
                {
                    Id = v.Id,
                    Type = v.Type,
                    RegNum = v.RegNum,
                    ArrivalTime = v.ArrivalTime,
                    Owner = v.Member.FullName,
                    ParkingNumber = v.VehicleModelParkingSpaces.Select(p => p.ParkingSpace.ParkingNum).ToList()
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
                .Select(v => new VehicleDetailsViewModel
                {
                    Id = v.Id,
                    Type = v.Type,
                    RegNum = v.RegNum,
                    Brand = v.Brand,
                    Model = v.Model,
                    Color = v.Color,
                    NumberWeels = v.NumWheels,
                    Owner = v.Member.FullName,
                    ArrivalTime = v.ArrivalTime

                })
                .FirstOrDefaultAsync(v => v.Id == id);
                

            if (vehicleModel == null)
            {
                return NotFound();
            }

            return View(vehicleModel);
        }

        // GET: VehicleModels2/Create
        public IActionResult Create()
        {
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
                var capacity = (int)db.VehicleType.Find((int)viewmodel.Type + 1).Capacity;
                vehicle = new VehicleModel
                {
                    ArrivalTime = DateTime.Now,
                    Brand = viewmodel.Brand,
                    Color = viewmodel.Color,
                    Model = viewmodel.Model,
                    NumWheels = viewmodel.NumWheels,
                    RegNum = viewmodel.RegNum.ToUpper(),
                    Type = viewmodel.Type,
                    VehicleTypeId = (int)viewmodel.Type + 1,
                    MemberId = viewmodel.MemberId,

                };

                int firstFree = GetFirstFreeParking(capacity);
                if (firstFree != 0)
                {
                    db.Add(vehicle);

                    for (int i = 0; i < capacity; i++)
                    {
                        var parkingSpace = db.ParkingSpace.Where(p => p.Id == firstFree + i).FirstOrDefault();
                        parkingSpace.ParkingNum = firstFree + i;
                        var vehicleSpace = new VehicleModelParkingSpace();
                        vehicleSpace.VehicleModel = vehicle;
                        vehicleSpace.ParkingSpace = parkingSpace;


                        db.Add(vehicleSpace);

                        await db.SaveChangesAsync();

                    }

                }

                db.VehicleModel.Include(v => v.VehicleModelParkingSpaces).ThenInclude(v => v.ParkingSpace);
                var space = parkingService.GetCurrentParking();
                var vehicleCapacity = parkingCapacityService.GetVehicleCapacity();

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

            var vehicleEditViewModel = await db.VehicleModel
                .Include(m => m.Member)
                .Select(v => new VehicleEditViewModel
                {
                    Id = v.Id,
                    Color = v.Color,
                    Brand = v.Brand,
                    Model = v.Model,
                    MemberId = v.MemberId
                    
                }).FirstOrDefaultAsync(v => v.Id == id);

            if (vehicleEditViewModel == null)
            {
                return NotFound();
            }
            
            return View(vehicleEditViewModel);
        }

        // POST: VehicleModels2/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VehicleEditViewModel vehicleEditViewModel)
        {
            if (id != vehicleEditViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var vehicle = new VehicleModel
                {
                    Id = vehicleEditViewModel.Id,
                    Color = vehicleEditViewModel.Color,
                    Brand = vehicleEditViewModel.Brand,
                    Model = vehicleEditViewModel.Model,
                    MemberId = vehicleEditViewModel.MemberId   
                };
                try
                {
                    db.Entry(vehicle).State = EntityState.Modified;
                    db.Entry(vehicle).Property(v => v.ArrivalTime).IsModified = false;
                    db.Entry(vehicle).Property(v => v.RegNum).IsModified = false;
                    db.Entry(vehicle).Property(v => v.Type).IsModified = false;
                    db.Entry(vehicle).Property(v => v.VehicleTypeId).IsModified = false;

                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleModelExists(vehicle.Id))
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
            
            return View(vehicleEditViewModel);
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


            await db.VehicleModelParkingSpaces.Include(p => p.ParkingSpace)
                .Where(v => v.VehicleModelId == id).ForEachAsync(p => p.ParkingSpace.ParkingNum = null);


            db.VehicleModel.Remove(vehicleModel);



            //await db.VehicleModelParkingSpaces.Include(v => v.ParkingSpace).FirstOrDefaultAsync(v => v.VehicleModelId == id);
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
