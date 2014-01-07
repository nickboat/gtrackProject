using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.vehicle;

namespace gtrackProject.Repositories.vehicle.IRepos
{
    public interface ILpTypeRepository
    {
        IQueryable<LpType> GetAll();
        Task<LpType> Get(int id);
        Task<LpType> Add(LpType item);
        Task<bool> Update(LpType item);
        Task<bool> Remove(int id);
    }
}
