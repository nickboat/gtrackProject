using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.order;

namespace gtrackProject.Repositories.order.IRepos
{
    public interface IOrderRepository
    {
        IQueryable<Order> GetAll();
        Task<Order> Get(int id);
        Task<Order> Add(Order item);
        Task<bool> Update(Order item);
        Task<bool> Remove(int id);
        Task<bool> UserActive(int orderId, string aspId, bool isQC);
    }
}
