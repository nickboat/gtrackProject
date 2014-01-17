using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gtrackProject.Models.order;

namespace gtrackProject.Repositories.order.IRepos
{
    public interface IOdExtendTypeRepository
    {
        IQueryable<OrderExtendType> GetAll();
        Task<OrderExtendType> Get(byte id);
        Task<OrderExtendType> Add(OrderExtendType item);
        Task<bool> Update(OrderExtendType item);
        Task<bool> Remove(byte id);
    }
}
