using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.product;

namespace gtrackProject.Repositories.product.IRepos
{
    public interface ILogSimRepository
    {
        IQueryable<LogSim> GetAll();
        Task<LogSim> Get(int id);
        Task<LogSim> Add(LogSim item);
        //Task<bool> Update(LogSim item);
        //Task<bool> Remove(int id);
    }
}
