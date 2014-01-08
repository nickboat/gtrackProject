using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.vehicle;

namespace gtrackProject.Repositories.vehicle.IRepos
{
    public interface IVehicleHeadTypeRepository
    {
        IQueryable<VehicleHeadType> GetAll();
        Task<VehicleHeadType> Get(byte id);
        Task<VehicleHeadType> Add(VehicleHeadType item);
        Task<bool> Update(VehicleHeadType item);
        Task<bool> Remove(byte id);
    }
}
