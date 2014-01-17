using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.order;

namespace gtrackProject.Repositories.order.IRepos
{
    public interface IFixOrderRepository
    {
        IQueryable<FixOrders> GetAll();
        Task<FixOrders> Get(int id);
        Task<FixOrders> Add(FixOrders item);
        Task<bool> Update(FixOrders item);
        Task<bool> Remove(int id);
    }
}
