using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.vehicle;

namespace gtrackProject.Repositories.vehicle.IRepos
{
    public interface IVehicleOganizeRepository
    {
        IQueryable<VehicleOganize> GetAll();
        Task<VehicleOganize> Get(byte id);
        Task<VehicleOganize> Add(VehicleOganize item);
        Task<bool> Update(VehicleOganize item);
        Task<bool> Remove(byte id);
    }
}
