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

        
    }
}
