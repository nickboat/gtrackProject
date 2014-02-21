using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.order;

namespace gtrackProject.Repositories.order.IRepos
{
    public interface IFixOrderRepository
    {
        IQueryable<FixOrder> GetAll();
        Task<FixOrder> Get(int id);
        Task<FixOrder> Add(FixOrder item);
        Task<bool> Update(FixOrder item);
        Task<bool> Remove(int id);
    }
}
