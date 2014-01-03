using System.Collections.Generic;
using System.Threading.Tasks;
using gtrackProject.Models.account;

namespace gtrackProject.Repositories.account
{
    public interface IEmployeeAdminRepository
    {
        IEnumerable<EmployeeAdminModel> GetAll();
        Task<EmployeeAdminModel> Get(int id);
        Task<EmployeeAdminModel> Add(EmployeeAdminModel item);
        Task<bool> Remove(int id);
        Task<bool> Update(EmployeeAdminModel item);
    }
}