using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.driver;

namespace gtrackProject.Repositories.driver.IRepos
{
    public interface IDriverCateRepository
    {
        IQueryable<DriverCategory> GetAll();
        Task<DriverCategory> Get(byte id);
        Task<DriverCategory> Add(DriverCategory item);
        Task<bool> Update(DriverCategory item);
        Task<bool> Remove(byte id);
    }
}
