using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.product;

namespace gtrackProject.Repositories.product.IRepos
{
    public interface ISimFeeTypeRepository
    {
        IQueryable<SimFeeType> GetAll();
        Task<SimFeeType> Get(byte id);
        Task<SimFeeType> Add(SimFeeType item);
        Task<bool> Update(SimFeeType item);
        Task<bool> Remove(byte id);
    }
}
