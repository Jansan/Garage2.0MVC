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
        public Garage2_0MVCContext(DbContextOptions<Garage2_0MVCContext> options)
            : base(options)
        {
        }

        public DbSet<VehicleModel> VehicleModel { get; set; }

        public DbSet<Member> Member { get; set; }

        public DbSet<VehicleType> VehicleType { get; set; }

        public DbSet<VehicleModelParkingSpace> VehicleModelParkingSpaces { get; set; }
        public DbSet<ParkingSpace> ParkingSpace { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleType>()
                .HasData(
                    new VehicleType { Id = 1, Type = VehicleTypeEnum.Car, Capacity = 1 },
                    new VehicleType { Id = 2, Type = VehicleTypeEnum.Motorcycle, Capacity = 0.3 },
                    new VehicleType { Id = 3, Type = VehicleTypeEnum.Bus, Capacity = 2 },
                    new VehicleType { Id = 4, Type = VehicleTypeEnum.Boat, Capacity = 2 },
                    new VehicleType { Id = 5, Type = VehicleTypeEnum.Airplane, Capacity = 3 }
                );

            for (int i = 1; i <= 20; i++)
            {
            modelBuilder.Entity<ParkingSpace>()
               .HasData(
                new  ParkingSpace{ Id = i,  ParkingNum = null}
               );
            }
        }
    }
}
