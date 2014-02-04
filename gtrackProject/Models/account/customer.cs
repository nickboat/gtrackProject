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
        public string FullName { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        [JsonIgnore]
        public virtual Hd Hd { get; set; }
    }
}
