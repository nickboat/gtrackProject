using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.universe;

namespace gtrackProject.Repositories.universe.IRepos
{
    public interface IUnDisplayRepository
    {
        IQueryable<UnDisplayStatus> GetAll();
        Task<UnDisplayStatus> Get(byte id);
        Task<UnDisplayStatus> Add(UnDisplayStatus item);
        Task<bool> Update(UnDisplayStatus item);
        Task<bool> Remove(byte id);
    }
}
