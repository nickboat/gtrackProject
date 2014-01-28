using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.product;

namespace gtrackProject.Repositories.product.IRepos
{
    public interface IGpsMemoryRepository
    {
        IQueryable<ProductGpsMemoryStatus> GetAll();
        Task<ProductGpsMemoryStatus> Get(byte id);
        Task<ProductGpsMemoryStatus> Add(ProductGpsMemoryStatus item);
        Task<bool> Update(ProductGpsMemoryStatus item);
        Task<bool> Remove(byte id);
    }
}
