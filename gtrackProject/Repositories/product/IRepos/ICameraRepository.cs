using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.product;

namespace gtrackProject.Repositories.product.IRepos
{
    public interface ICameraRepository
    {
        IQueryable<ProductCamera> GetAll();
        Task<ProductCamera> Get(int id);
        Task<ProductCamera> Add(ProductCamera item);
        Task<bool> Update(ProductCamera item);
        Task<bool> Remove(int id);
    }
}
