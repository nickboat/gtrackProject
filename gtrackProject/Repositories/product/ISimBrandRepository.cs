using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models;

namespace gtrackProject.Repositories.product
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
