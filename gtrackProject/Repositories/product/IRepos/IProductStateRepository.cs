using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.product;

namespace gtrackProject.Repositories.product.IRepos
{
    public interface IProductStateRepository
    {
        IQueryable<ProductProcessState> GetAll();
        Task<ProductProcessState> Get(byte id);
        Task<ProductProcessState> Add(ProductProcessState item);
        Task<bool> Update(ProductProcessState item);
        Task<bool> Remove(byte id);
    }
}
