using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.vehicle;

namespace gtrackProject.Repositories.vehicle.IRepos
{
    public interface ILogMoveVehicleRepository
    {
        IQueryable<LogMoveVehicle> GetAll();
        Task<LogMoveVehicle> Get(int id);
        Task<LogMoveVehicle> Add(LogMoveVehicle item);
    }
}
