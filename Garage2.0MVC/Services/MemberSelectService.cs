using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Garage2._0MVC.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Garage2._0MVC.Services
{
    public class MemberSelectService : IMemberSelectService
    {
        private readonly Garage2_0MVCContext db;

        public MemberSelectService(Garage2_0MVCContext db)
        {
            this.db = db;
        }
        public async Task<IEnumerable<SelectListItem>> GetMembersAsync()
        {
            return await db.Member
                        .Select(m => new SelectListItem
                        {
                            Text = m.Email.ToString(),
                            Value = m.Id.ToString()
                        })
                        .ToListAsync();
        }
    }
}
