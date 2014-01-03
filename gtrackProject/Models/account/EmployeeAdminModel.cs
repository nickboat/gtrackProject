using System.ComponentModel.DataAnnotations.Schema;

namespace gtrackProject.Models.account
{
    [NotMapped]
    public class EmployeeAdminModel : Employee
    {
        public string UserName { get; set; }

        public string[] Roles { get; set; }
    }
}