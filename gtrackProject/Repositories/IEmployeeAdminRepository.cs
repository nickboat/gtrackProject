using System.Threading.Tasks;
using gtrackProject.Models.account;
using System.Collections.Generic;
namespace gtrackProject.Repositories
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