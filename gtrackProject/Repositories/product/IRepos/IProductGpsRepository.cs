using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.product;

namespace gtrackProject.Repositories.product.IRepos
{
    public interface IProductGpsRepository
    {
        IQueryable<Gps> GetAll();
        Task<Gps> Get(int id);
        Task<Gps> Add(Gps item);
        Task<bool> Update(Gps item);
        Task<bool> Remove(int id);
    }
}
