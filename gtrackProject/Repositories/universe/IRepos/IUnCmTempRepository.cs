using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.universe;

namespace gtrackProject.Repositories.universe.IRepos
{
    public interface IUnCmTempRepository
    {
        IQueryable<UnCmTemp> GetAll();
        Task<UnCmTemp> Get(string id);
        Task<UnCmTemp> Add(UnCmTemp item);
        Task<bool> Update(UnCmTemp item);
        Task<bool> Remove(string id);
    }
}
