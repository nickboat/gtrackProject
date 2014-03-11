using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.product;

namespace gtrackProject.Repositories.product.IRepos
{
    interface ISimRepository
    {
        IQueryable<Sim> GetAll();
        Task<Sim> Get(int id);
        Task<Sim> Add(Sim item);
        Task<bool> Update(Sim item);
        Task<bool> Remove(int id);
    }
}
