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
namespace Garage2._0MVC.Controllers
{
    public class VehicleModelsController : Controller
    {
        private readonly Garage2_0MVCContext db;
        public const int PARKING_CAPACITY = 3;

        public VehicleModelsController(Garage2_0MVCContext context)
        {
            db = context;
        }

        // GET: VehicleModels
        public async Task<IActionResult> Index()
        {
            var iCollection = new IndexCollectionViewModel();
            iCollection.Vehicles = await db.VehicleModel.ToListAsync();
            var totalVehicles = db.VehicleModel.Count();
            iCollection.ParkingSpacesLeft = PARKING_CAPACITY - totalVehicles;
            iCollection.TotalSpaces = PARKING_CAPACITY;
            return View(iCollection);
        }

        // GET: Vehicles
        public async Task<IActionResult> Vehicles()
        {
            var vCollection = new VehiclesCollectionViewModel();
            var totalVehicles = db.VehicleModel.Count();
            var model = await db.VehicleModel.Select(v => new VehicleViewModel
            {
                Id = v.Id,
                VehicleType = v.Type,
                RegNum = v.RegNum,
                ArrivalTime = v.ArrivalTime,
            }).ToListAsync();

            vCollection.ParkingSpacesLeft = PARKING_CAPACITY - totalVehicles;
            vCollection.Vehicles = model;
            vCollection.TotalSpaces = PARKING_CAPACITY;

            return View(vCollection);
        }



        // GET: VehicleModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleModel = await db.VehicleModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleModel == null)
            {
                return NotFound();
            }

            return View(vehicleModel);
        }
        public async Task<IActionResult> Filter(string regNum)
        {
            IQueryable<VehicleViewModel> model;
            if (string.IsNullOrWhiteSpace(regNum))
            {
                model = db.VehicleModel.Select(v => new VehicleViewModel
                {
                    VehicleType = v.Type,
                    ArrivalTime = v.ArrivalTime,
                    RegNum = v.RegNum
                });
            }
            else
            {
                model = db.VehicleModel
                    .Where(v => v.RegNum.Contains(regNum))
                    .Select(v => new VehicleViewModel
                    {
                        VehicleType = v.Type,
                        ArrivalTime = v.ArrivalTime,
                        RegNum = v.RegNum
                    });
            }

            return View(nameof(Vehicles), await model.ToListAsync());
        }

        public async Task<IActionResult> Statistics()
        {

            var model = db.VehicleModel.GroupBy(v => v.Type)
                .Select(v => new StatisticsViewModel
                {
                    Type = v.Key,
                    Count = v.Count()
                }); ;
            return View(await model.ToListAsync());
        }

        // GET: VehicleModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VehicleModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,RegNum,Color,Brand,Model,NumWheels,ArrivalTime")] VehicleModel vehicleModel)
        {
            if (ModelState.IsValid)
            {

                vehicleModel.ArrivalTime = DateTime.Now;
                vehicleModel.RegNum = vehicleModel.RegNum.ToUpper();
                db.Add(vehicleModel);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleModel);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult UniqueRegNum(string regNum)
        {
            if (db.VehicleModel.Any(v => v.RegNum == regNum))
            {
                return Json($"That registration number already is among the parked vehicles.");
            }
            return Json(true);
        }

        // GET: VehicleModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editViewModel = await db.VehicleModel
                .Select(e => new EditViewModel
                {
                    Id = e.Id,
                    Type = e.Type,
                    Color = e.Color,
                    Brand = e.Brand,
                    Model = e.Model,
                    NumWheels = e.NumWheels
                }).FirstOrDefaultAsync(v => v.Id == id);

            if (editViewModel == null)
            {
                return NotFound();
            }
            return View(editViewModel);
        }

        // POST: VehicleModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditViewModel editViewModel)
        {
            if (id != editViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var vehicle = new VehicleModel
                {
                    Id = editViewModel.Id,
                    Type = editViewModel.Type,
                    Color = editViewModel.Color,
                    Brand = editViewModel.Brand,
                    Model = editViewModel.Model,
                    NumWheels = editViewModel.NumWheels
                };
                try
                {
                    db.Entry(vehicle).State = EntityState.Modified;
                    db.Entry(vehicle).Property(v => v.ArrivalTime).IsModified = false;
                    db.Entry(vehicle).Property(v => v.RegNum).IsModified = false;

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
            return View(editViewModel);
        }

        // GET: VehicleModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleModel = await db.VehicleModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleModel == null)
            {
                return NotFound();
            }

            return View(vehicleModel);
        }

        // POST: VehicleModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicleModel = await db.VehicleModel.FindAsync(id);
            db.VehicleModel.Remove(vehicleModel);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Receipt(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleModel = await db.VehicleModel.FirstOrDefaultAsync(m => m.Id == id);

            if (vehicleModel == null)
            {
                return NotFound();
            }

            return View(vehicleModel);
        }

        private bool VehicleModelExists(int id)
        {
            return db.VehicleModel.Any(e => e.Id == id);
        }
    }
}
