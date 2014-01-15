using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gtrackProject.Models;
using gtrackProject.Models.universe;

namespace gtrackProject.Repositories.universe.IRepos
{
    public interface IUnCmGpsRepository
    {
        IQueryable<UnCmGps> GetAll();
        Task<UnCmGps> Get(string id);
        Task<UnCmGps> Add(UnCmGps item);
        Task<bool> Update(UnCmGps item);
        Task<bool> Remove(string id);
    }
}
