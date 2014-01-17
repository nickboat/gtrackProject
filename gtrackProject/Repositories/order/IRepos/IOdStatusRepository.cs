using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.order;

namespace gtrackProject.Repositories.order.IRepos
{
    public interface IOdStatusRepository
    {
        IQueryable<OrderStatus> GetAll();
        Task<OrderStatus> Get(byte id);
        Task<OrderStatus> Add(OrderStatus item);
        Task<bool> Update(OrderStatus item);
        Task<bool> Remove(byte id);
    }
}
