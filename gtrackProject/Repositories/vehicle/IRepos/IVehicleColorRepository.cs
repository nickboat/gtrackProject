using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.vehicle;

namespace gtrackProject.Repositories.vehicle.IRepos
{
    public interface IVehicleColorRepository
    {
        IQueryable<VehicleColor> GetAll();
        Task<VehicleColor> Get(int id);
        Task<VehicleColor> Add(VehicleColor item);
        Task<bool> Update(VehicleColor item);
        Task<bool> Remove(int id);
    }
}
