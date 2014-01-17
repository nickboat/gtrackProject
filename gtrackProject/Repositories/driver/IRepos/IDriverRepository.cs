using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.driver;

namespace gtrackProject.Repositories.driver.IRepos
{
    public interface IDriverRepository
    {
        IQueryable<Driver> GetAll();
        Task<Driver> Get(int id);
        Task<Driver> Add(Driver item);
        Task<bool> Update(Driver item);
        Task<bool> Remove(int id);
    }
}
