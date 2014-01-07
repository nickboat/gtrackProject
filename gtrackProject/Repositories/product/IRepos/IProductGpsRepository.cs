using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.product;

namespace gtrackProject.Repositories.product.IRepos
{
    public interface IProductGpsRepository
    {
        IQueryable<ProductGps> GetAll();
        Task<ProductGps> Get(int id);
        Task<ProductGps> Add(ProductGps item);
        Task<bool> Update(ProductGps item);
        Task<bool> Remove(int id);
    }
}
