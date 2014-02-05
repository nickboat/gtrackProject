using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gtrackProject.Models.account
{
    [NotMapped]
    public class CustomerModel : Customer
    {
        [StringLength(50, MinimumLength = 6, ErrorMessage = "UserName must be atleast 6 characters")]
        public string UserName { get; set; }
    }
}