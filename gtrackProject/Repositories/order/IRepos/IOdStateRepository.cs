using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.order;

namespace gtrackProject.Repositories.order.IRepos
{
    public interface IOdStateRepository
    {
        IQueryable<OrderProcessState> GetAll();
        Task<OrderProcessState> Get(byte id);
        Task<OrderProcessState> Add(OrderProcessState item);
        Task<bool> Update(OrderProcessState item);
        Task<bool> Remove(byte id);
    }
}
