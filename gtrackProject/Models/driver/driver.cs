using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using gtrackProject.Models.universe;
using Newtonsoft.Json;

namespace gtrackProject.Models.driver
{
    public class Driver
    {
        public Driver()
        {
            Universes = new List<Universe>();
            LogCardReaders = new List<LogCardReader>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [Range(1000000000000, 9999999999999)]
        public long IdCard { get; set; }
        [Range(1000,9999)]
        public short? ExpireCard { get; set; }
        [StringLength(6, MinimumLength = 2, ErrorMessage = "TitleName must be atleast 2 characters")]
        public string TitleName { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "FirstName must be atleast 2 characters")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "LastNmae must be atleast 2 characters")]
        public string LastNmae { get; set; }
        [Required] public System.DateTime BirthDate { get; set; }
        [Required]
        [RegularExpression(@"^(m|f)$", ErrorMessage = "Please use 'm' = male, 'f' = female")]
        public string Gender { get; set; }
        [Required]
        [Range(10000000,99999999)]
        public int DriverIdCard { get; set; }
        [Required]
        [Range(10000, 99999)]
        public short ZipCode { get; set; }
        [Required]
        [ForeignKey("DriverCategory")]
        public byte CategoryId { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public string AspId { get; set; }//Asp_Id


        [JsonIgnore]
        //[IgnoreDataMember]
        public virtual DriverCategory DriverCategory { get; set; }


        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<Universe> Universes { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<LogCardReader> LogCardReaders { get; set; }
    }
}
