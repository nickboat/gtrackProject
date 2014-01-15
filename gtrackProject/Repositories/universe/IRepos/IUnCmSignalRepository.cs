using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.universe;

namespace gtrackProject.Repositories.universe.IRepos
{
    public interface IUnCmSignalRepository
    {
        IQueryable<UnCmSignal> GetAll();
        Task<UnCmSignal> Get(string id);
        Task<UnCmSignal> Add(UnCmSignal item);
        Task<bool> Update(UnCmSignal item);
        Task<bool> Remove(string id);
    }
}
