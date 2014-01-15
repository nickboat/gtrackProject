using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.universe;

namespace gtrackProject.Repositories.universe.IRepos
{
    public interface IUnCmMeterRepository
    {
        IQueryable<UnCmMeter> GetAll();
        Task<UnCmMeter> Get(string id);
        Task<UnCmMeter> Add(UnCmMeter item);
        Task<bool> Update(UnCmMeter item);
        Task<bool> Remove(string id);
    }
}
