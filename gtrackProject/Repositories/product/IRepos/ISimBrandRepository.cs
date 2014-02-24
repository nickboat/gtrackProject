using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.product;

namespace gtrackProject.Repositories.product.IRepos
{
    public interface ISimBrandRepository
    {
        IQueryable<SimNetwork> GetAll();
        Task<SimNetwork> Get(byte id);
        Task<SimNetwork> Add(SimNetwork item);
        Task<bool> Update(SimNetwork item);
        Task<bool> Remove(byte id);
    }
}
