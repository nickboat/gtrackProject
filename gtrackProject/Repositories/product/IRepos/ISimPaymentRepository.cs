using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.product;

namespace gtrackProject.Repositories.product.IRepos
{
    public interface ISimPaymentRepository
    {
        IQueryable<SimPaymentType> GetAll();
        Task<SimPaymentType> Get(byte id);
        Task<SimPaymentType> Add(SimPaymentType item);
        Task<bool> Update(SimPaymentType item);
        Task<bool> Remove(byte id);
    }
}
