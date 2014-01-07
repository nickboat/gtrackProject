using System.Collections.Generic;
using System.Threading.Tasks;
using gtrackProject.Models.account;

namespace gtrackProject.Repositories.account.IRepos
{
    public interface ICustomerRepository
    {
        IEnumerable<CustomerModel> GetByHd(int hdId);
        Task<CustomerModel> Get(int id);
        Task<CustomerModel> Add(CustomerModel item);
        Task<bool> Update(CustomerModel item);
        Task<bool> Remove(int id);
    }
}
