using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.product;

namespace gtrackProject.Repositories.product.IRepos
{
    public interface ICameraStatusRepository
    {
        IQueryable<ProductCameraStatus> GetAll();
        Task<ProductCameraStatus> Get(byte id);
        Task<ProductCameraStatus> Add(ProductCameraStatus item);
        Task<bool> Update(ProductCameraStatus item);
        Task<bool> Remove(byte id);
    }
}
