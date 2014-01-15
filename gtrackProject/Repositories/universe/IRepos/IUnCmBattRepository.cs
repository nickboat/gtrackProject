using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.universe;

namespace gtrackProject.Repositories.universe.IRepos
{
    public interface IUnCmBattRepository
    {
        IQueryable<UnCmBatt> GetAll();
        Task<UnCmBatt> Get(string id);
        Task<UnCmBatt> Add(UnCmBatt item);
        Task<bool> Update(UnCmBatt item);
        Task<bool> Remove(string id);
    }
}
