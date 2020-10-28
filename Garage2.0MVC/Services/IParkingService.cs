using System.Threading.Tasks;

namespace Garage2._0MVC.Services
{
    public interface IParkingService
    {
        int GetCurrentParking();
        int GetTotalParking();
    }
}