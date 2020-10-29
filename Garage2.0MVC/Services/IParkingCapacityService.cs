using System.Collections.Generic;
using Garage2._0MVC.Models;

namespace Garage2._0MVC.Services
{
    public interface IParkingCapacityService
    {
        IEnumerable<VehicleType> GetVehicleCapacity();
    }
}