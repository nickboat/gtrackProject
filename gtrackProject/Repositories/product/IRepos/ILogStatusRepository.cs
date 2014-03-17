using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.product;

namespace gtrackProject.Repositories.product.IRepos
{
    public interface ILogStatusRepository
    {
        IQueryable<LogStatus> GetAll();
        Task<LogStatus> Get(int id);
        Task<LogStatus> Add(LogStatus item);
        Task<bool> Update(LogStatus item);
        Task<bool> Remove(int id);
    }
}
