using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.product;

namespace gtrackProject.Repositories.product.IRepos
{
    public interface IGpsStateRepository
    {
        IQueryable<ProductGpsProcessState> GetAll();
        Task<ProductGpsProcessState> Get(byte id);
        Task<ProductGpsProcessState> Add(ProductGpsProcessState item);
        Task<bool> Update(ProductGpsProcessState item);
        Task<bool> Remove(byte id);
    }
}
