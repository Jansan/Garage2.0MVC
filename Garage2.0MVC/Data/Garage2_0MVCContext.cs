using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Garage2._0MVC.Models;

namespace Garage2._0MVC.Data
{
    public class Garage2_0MVCContext : DbContext
    {
        public Garage2_0MVCContext (DbContextOptions<Garage2_0MVCContext> options)
            : base(options)
        {
        }

        public DbSet<Garage2._0MVC.Models.VehicleModel> VehicleModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleModel>()
                .HasData(
                    new VehicleModel { Id = 1, Type = VehicleTypeEnum.Car, RegNum = "ABC123", Color = "Red", Brand = "Volvo", Model = "V70", NumWheels = 4, ArrivalTime = DateTime.Now},
                    new VehicleModel { Id = 2, Type = VehicleTypeEnum.Bus, RegNum = "GHT253", Color = "Blue", Brand = "Saab", Model = "T20", NumWheels = 6, ArrivalTime = DateTime.Now},
                    new VehicleModel { Id = 3, Type = VehicleTypeEnum.Boat, RegNum = "TYU589", Color = "Black", Brand = "BMW", Model = "800", NumWheels = 0, ArrivalTime = DateTime.Now},
                    new VehicleModel { Id = 4, Type = VehicleTypeEnum.Airplane, RegNum = "SK1420", Color = "Silver", Brand = "SAS", Model = "737", NumWheels = 6, ArrivalTime = DateTime.Now}
                    
                );
        }

        public DbSet<Garage2._0MVC.Models.VehicleType> VehicleType { get; set; }
    }
}
