using gtrackProject.Models.account;
using System.Collections.Generic;
namespace gtrackProject.Repositories
{
    public interface IEmployeeAdminRepository
    {
        IEnumerable<EmployeeAdminModel> GetAll();
        EmployeeAdminModel Get(int id);
        EmployeeAdminModel Add(EmployeeAdminModel item);
        bool Remove(int id);
        bool Update(EmployeeAdminModel item);
    }
}