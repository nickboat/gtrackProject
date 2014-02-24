using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.order;

namespace gtrackProject.Repositories.order.IRepos
{
    public interface IOdStateRepository
    {
        IQueryable<OrderState> GetAll();
        Task<OrderState> Get(byte id);
        Task<OrderState> Add(OrderState item);
        Task<bool> Update(OrderState item);
        Task<bool> Remove(byte id);
    }
}
