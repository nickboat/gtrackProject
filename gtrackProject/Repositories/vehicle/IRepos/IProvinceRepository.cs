using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gtrackProject.Models;
using gtrackProject.Models.vehicle;

namespace gtrackProject.Repositories.vehicle.IRepos
{
    public interface IProvinceRepository
    {
        IQueryable<Province> GetAll();
        Task<Province> Get(int id);
        Task<Province> Add(Province item);
        Task<bool> Update(Province item);
        Task<bool> Remove(int id);
    }
}
