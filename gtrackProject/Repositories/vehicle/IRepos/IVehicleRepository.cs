using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.vehicle;

namespace gtrackProject.Repositories.vehicle.IRepos
{
    public interface IVehicleRepository
    {
        IQueryable<Vehicle> GetAll();
        Task<Vehicle> Get(int id);
        Task<Vehicle> Add(Vehicle item);
        Task<bool> Update(Vehicle item);
        Task<bool> Remove(int id);
    }
}
