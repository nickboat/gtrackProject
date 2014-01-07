using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.product;

namespace gtrackProject.Repositories.product.IRepos
{
    public interface ISimBrandRepository
    {
        IQueryable<SimBrand> GetAll();
        Task<SimBrand> Get(byte id);
        Task<SimBrand> Add(SimBrand item);
        Task<bool> Update(SimBrand item);
        Task<bool> Remove(byte id);
    }
}
