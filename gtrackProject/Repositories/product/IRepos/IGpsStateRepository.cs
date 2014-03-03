using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.product;

namespace gtrackProject.Repositories.product.IRepos
{
    public interface IGpsStateRepository
    {
        IQueryable<GpsState> GetAll();
        Task<GpsState> Get(byte id);
        Task<GpsState> Add(GpsState item);
        Task<bool> Update(GpsState item);
        Task<bool> Remove(byte id);
    }
}
