using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.universe;

namespace gtrackProject.Repositories.universe.IRepos
{
    public interface IUniverseRepository
    {
        IQueryable<Universe> GetAll();
        Task<Universe> Get(int id);
        //Task<Universe> Add(Universe item);
        Task<bool> UpdateGps(Universe item);
        Task<bool> UpdateData(Universe item);
        //Task<bool> UpdateDataString(string item);
        Task<bool> GetOffGps(int id);
        //Task<bool> Remove(int id);
    }
}
