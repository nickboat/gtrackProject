using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.vehicle;

namespace gtrackProject.Repositories.vehicle.IRepos
{
    public interface IVehicleBrandRepository
    {
        IQueryable<VehicleBrand> GetAll();
        Task<VehicleBrand> Get(byte id);
        Task<VehicleBrand> Add(VehicleBrand item);
        Task<bool> Update(VehicleBrand item);
        Task<bool> Remove(byte id);
    }
}
