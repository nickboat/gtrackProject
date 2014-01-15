using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.universe;

namespace gtrackProject.Repositories.universe.IRepos
{
    public interface IUnCmCommRepository
    {
        IQueryable<UnCmComm> GetAll();
        Task<UnCmComm> Get(string id);
        Task<UnCmComm> Add(UnCmComm item);
        Task<bool> Update(UnCmComm item);
        Task<bool> Remove(string id);
    }
}
