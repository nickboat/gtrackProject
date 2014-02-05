using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace gtrackProject.Models.account
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string Asp_Id { get; set; }
        [Required]
        [ForeignKey("Hd")]
        public short Hd_Id { get; set; }
        [StringLength(100, MinimumLength = 4, ErrorMessage = "FullName must be atleast 4 characters")]
        public string FullName { get; set; }
        [Required]
        [RegularExpression(@"(?(^02)^02\d{7}|^0\d{9})$", ErrorMessage = "Invalid Phone Number")]
        public string Phone { get; set; }
        [RegularExpression(@"^[\w-]+(\.[\w-]+)*@([a-z0-9-]+(\.[a-z0-9-]+)*?\.[a-z]{2,6}|(\d{1,3}\.){3}\d{1,3})(:\d{4})?$", ErrorMessage = "Invalid Email address")]
        public string Email { get; set; }
        [StringLength(100, MinimumLength = 4, ErrorMessage = "CompanyName must be atleast 4 characters")]
        public string CompanyName { get; set; }
        [JsonIgnore]
        public virtual Hd Hd { get; set; }
    }
}
