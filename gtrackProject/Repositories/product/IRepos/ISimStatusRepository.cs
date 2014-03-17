using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.product;

namespace gtrackProject.Repositories.product.IRepos
{
    public interface ISimStatusRepository
    {
        IQueryable<SimStatus> GetAll();
        Task<SimStatus> Get(byte id);
        Task<SimStatus> Add(SimStatus item);
        Task<bool> Update(SimStatus item);
        Task<bool> Remove(byte id);
    }
}
