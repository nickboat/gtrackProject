using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gtrackProject.Models;
using gtrackProject.Models.vehicle;

namespace gtrackProject.Repositories.vehicle.IRepos
{
    public interface IVehicleHeadTypeRepository
    {
        IQueryable<VehicleHeadType> GetAll();
        Task<VehicleHeadType> Get(int id);
        Task<VehicleHeadType> Add(VehicleHeadType item);
        Task<bool> Update(VehicleHeadType item);
        Task<bool> Remove(int id);
    }
}
