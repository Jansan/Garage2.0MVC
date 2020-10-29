using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Garage2._0MVC.Data;
using Garage2._0MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace Garage2._0MVC.Services
{
    public class ParkingCapacityService : IParkingCapacityService
    {
        private readonly Garage2_0MVCContext db;
        private IEnumerable<VehicleType> vehicleTypes;

        public ParkingCapacityService(Garage2_0MVCContext db)
        {
            this.db = db;
        }

        public IEnumerable<VehicleType> GetVehicleCapacity()
        {
            vehicleTypes = vehicleTypes is null ? db.VehicleType.ToList() : vehicleTypes;
            return vehicleTypes;
        }
    }
}
