using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Garage2._0MVC.Data;
using Garage2._0MVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Garage2._0MVC.Services
{
    public class TypeSelectService : ITypeSelectService
    {
        private readonly Garage2_0MVCContext db;
        private readonly IParkingService parkingService;
        private readonly IParkingCapacityService parkingCapacityService;

        public TypeSelectService(Garage2_0MVCContext db, IParkingService parkingService, IParkingCapacityService parkingCapacityService)
        {
            this.db = db;
            this.parkingService = parkingService;
            this.parkingCapacityService = parkingCapacityService;
        }

        
        public async Task<IEnumerable<SelectListItem>> GetTypesAsync()
        {
            var space = (double)parkingService.GetCurrentParking();
            var capacity = parkingCapacityService.GetVehicleCapacity();
            return await db.VehicleType
                        .Select
                        (v => new SelectListItem
                        {
                            Text = v.Type.ToString(),
                            Value = v.Type.ToString(),
                            Disabled = space<v.Capacity? true : false
                        })
                        .ToListAsync();
        }
    }
}
