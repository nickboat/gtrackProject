using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace gtrackProject.Repositories.account.IRepos
{
    public interface IRoleAdminRepository
    {
        IQueryable<IdentityRole> GetAll();
        Task<IdentityRole> Get(string id);
        Task<IdentityRole> Add(IdentityRole role);
        Task<bool> Update(IdentityRole role);
        Task<bool> Remove(string id);
    }
}
