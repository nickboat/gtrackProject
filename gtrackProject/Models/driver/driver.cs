using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using gtrackProject.Models.universe;
using Newtonsoft.Json;

namespace gtrackProject.Models.driver
{
    public class Driver
    {
        public Driver()
        {
            Universes = new List<Universe>();
        }
        [Key]
        public int Id { get; set; }
        [Required] public int IdCard { get; set; }
        [Range(1000,9999)]
        public short? ExpireCard { get; set; }
        public string TitleName { get; set; }
        [Required] public string FirstName { get; set; }
        [Required] public string LastNmae { get; set; }
        [Required] public System.DateTime BirthDate { get; set; }
        [Required] public string Gender { get; set; }
        [Required] public int DriverIdCard { get; set; }
        [Required] public short ZipCode { get; set; }
        [Required]
        [ForeignKey("DriverCategory")]
        public byte CategoryId { get; set; }
        public string AspId { get; set; }//Asp_Id
        [JsonIgnore]
        //[IgnoreDataMember]
        public virtual DriverCategory DriverCategory { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<Universe> Universes { get; set; }
    }
}
