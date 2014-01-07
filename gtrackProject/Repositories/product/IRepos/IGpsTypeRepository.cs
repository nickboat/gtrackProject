using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.product;

namespace gtrackProject.Repositories.product.IRepos
{
    public interface IGpsTypeRepository
    {
        IQueryable<ProductGpsType> GetAll();
        Task<ProductGpsType> Get(byte id);
        Task<ProductGpsType> Add(ProductGpsType item);
        Task<bool> Update(ProductGpsType item);
        Task<bool> Remove(byte id);
    }
}
