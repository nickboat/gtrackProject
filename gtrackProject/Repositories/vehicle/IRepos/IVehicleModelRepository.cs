using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gtrackProject.Models.vehicle;

namespace gtrackProject.Repositories.vehicle.IRepos
{
    public interface IVehicleModelRepository
    {
        IQueryable<VehicleModel> GetAll();
        Task<VehicleModel> Get(short id);
        Task<VehicleModel> Add(VehicleModel item);
        Task<bool> Update(VehicleModel item);
        Task<bool> Remove(short id);
    }
}
