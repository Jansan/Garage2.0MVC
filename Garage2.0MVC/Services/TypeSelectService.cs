using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Garage2._0MVC.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Garage2._0MVC.Services
{
    public class TypeSelectService : ITypeSelectService
    {
        private readonly Garage2_0MVCContext db;

        public TypeSelectService(Garage2_0MVCContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<SelectListItem>> GetTypesAsync()
        {
            return await db.VehicleType
                        .Select
                        (v => new SelectListItem
                        {
                            Text = v.Type.ToString(),
                            Value = v.Type.ToString()
                        })
                        .ToListAsync();
        }
    }
}
