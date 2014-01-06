using System.ComponentModel.DataAnnotations.Schema;

namespace gtrackProject.Models.account
{
    [NotMapped]
    public class CustomerModel : Customer
    {
        public string UserName { get; set; }
    }
}