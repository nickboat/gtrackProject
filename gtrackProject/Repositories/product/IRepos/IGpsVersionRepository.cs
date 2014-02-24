using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.product;

namespace gtrackProject.Repositories.product.IRepos
{
    public interface IGpsVersionRepository
    {
        IQueryable<GpsVersion> GetAll();
        Task<GpsVersion> Get(byte id);
        Task<GpsVersion> Add(GpsVersion item);
        Task<bool> Update(GpsVersion item);
        Task<bool> Remove(byte id);
    }
}
