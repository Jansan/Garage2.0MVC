using Garage2._0MVC.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2._0MVC.Services
{
    public class FilterService : IFilterService
    {
        private readonly Garage2_0MVCContext db;

        public FilterService(Garage2_0MVCContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<SelectListItem>> GetFilterAsync()
        {
            return await db.VehicleModel
                        .Select(v => v.Type)
                        .Distinct()
                        .Select
                        (v => new SelectListItem
                        {
                            Text = v.ToString(),
                            Value = v.ToString()
                        })
                        .ToListAsync();
        }
    }
}
