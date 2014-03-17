using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.product;

namespace gtrackProject.Repositories.product.IRepos
{
    public interface ILogMoveGpsRepository
    {
        IQueryable<LogMoveGps> GetAll();
        Task<LogMoveGps> Get(int id);
        Task<LogMoveGps> Add(LogMoveGps item);
        Task<bool> UpStatus(int id, int status);
    }
}
