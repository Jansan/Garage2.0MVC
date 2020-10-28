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
    public class VehicleModels2Controller : Controller
    {
        private readonly Garage2_0MVCContext db;

        public VehicleModels2Controller(Garage2_0MVCContext db)
        {
            this.db = db;
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
            ViewData["MemberId"] = new SelectList(db.Member, "Id", "Id");
            ViewData["VehicleTypeId"] = new SelectList(db.VehicleType, "Id", "Id");
            return View();
        }

        // POST: VehicleModels2/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,RegNum,Color,Brand,Model,NumWheels,ArrivalTime,VehicleTypeId,MemberId")] VehicleModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                db.Add(vehicleModel);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MemberId"] = new SelectList(db.Member, "Id", "Id", vehicleModel.MemberId);
            ViewData["VehicleTypeId"] = new SelectList(db.VehicleType, "Id", "Id", vehicleModel.VehicleTypeId);
            return View(vehicleModel);
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
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleModelExists(int id)
        {
            return db.VehicleModel.Any(e => e.Id == id);
        }
    }
}
