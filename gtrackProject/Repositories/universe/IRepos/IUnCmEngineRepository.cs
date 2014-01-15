using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.universe;

namespace gtrackProject.Repositories.universe.IRepos
{
    public interface IUnCmEngineRepository
    {
        IQueryable<UnCmEngine> GetAll();
        Task<UnCmEngine> Get(string id);
        Task<UnCmEngine> Add(UnCmEngine item);
        Task<bool> Update(UnCmEngine item);
        Task<bool> Remove(string id);
    }
}
