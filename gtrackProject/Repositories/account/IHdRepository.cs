using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gtrackProject.Models;
using gtrackProject.Models.account;

namespace gtrackProject.Repositories.account
{
    public interface IHdRepository
    {
        IQueryable<Hd> GetAll();
        Task<Hd> Get(int id);
        Task<Hd> Add(Hd item);
        Task<bool> Remove(int id);
        Task<bool> Update(Hd item);
    }
}
