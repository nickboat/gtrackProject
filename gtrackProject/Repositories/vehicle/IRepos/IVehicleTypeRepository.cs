using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gtrackProject.Models.vehicle;

namespace gtrackProject.Repositories.vehicle.IRepos
{
    public interface IVehicleTypeRepository
    {
        IQueryable<VehicleType> GetAll();
        Task<VehicleType> Get(byte id);
        Task<VehicleType> Add(VehicleType item);
        Task<bool> Update(VehicleType item);
        Task<bool> Remove(byte id);
    }
}
