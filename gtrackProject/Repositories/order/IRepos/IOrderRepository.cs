using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
