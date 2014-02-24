using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace gtrackProject.Models.product
{
    public class Sim
    {
        public Sim()
        {
            Gpses = new List<Gps>();
            LogSimOlds = new List<LogSim>();
            LogSimNews = new List<LogSim>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Invalid SIM Number")]
        public string Number { get; set; }
        [Required]
        [ForeignKey("SimNetwork")]
        public byte Network { get; set; }
        [Required]
        [ForeignKey("SimFeeType")]
        public byte FeeType { get; set; }
        [Required]
        [ForeignKey("SimStatus")]
        public byte Status { get; set; }


        [JsonIgnore]
        public virtual SimNetwork SimNetwork { get; set; }
        [JsonIgnore]
        public virtual SimFeeType SimFeeType { get; set; }
        [JsonIgnore]
        public virtual SimStatus SimStatus { get; set; }



        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<Gps> Gpses { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<LogSim> LogSimOlds { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<LogSim> LogSimNews { get; set; }
    }
}