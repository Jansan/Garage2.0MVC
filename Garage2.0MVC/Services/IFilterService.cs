using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2._0MVC.Services
{
    public interface IFilterService
    {
        Task<IEnumerable<SelectListItem>> GetFilterAsync();
    }
}
