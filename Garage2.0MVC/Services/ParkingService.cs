using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Garage2._0MVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Garage2._0MVC.Services
{
    public class ParkingService : IParkingService
    {
        private readonly Garage2_0MVCContext db;

        private const int totalParkingSpaces = 20;

        public ParkingService(Garage2_0MVCContext db)
        {
            this.db = db;
        }

        public int GetTotalParking()
        {
            var total = totalParkingSpaces;
            return total;

        }

        public int GetCurrentParking()
        {
            var vehicles = db.VehicleModel.Include(v => v.VehicleType).Select(v => v.VehicleType.Capacity).ToList();
            int sum = 0;

            foreach (var item in vehicles)
            {
                sum += item;
            }
            var space = totalParkingSpaces - sum;
            return space;

        }
    }
}
