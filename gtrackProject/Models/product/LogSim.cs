using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using gtrackProject.Models.account;
using Newtonsoft.Json;

namespace gtrackProject.Models.product
{
    public class LogSim
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("CreateByEmployee")]
        public int CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        [Required]
        [ForeignKey("Gps")]
        public int GpsId { get; set; }
        [Required]
        [ForeignKey("OldSim")]
        public int SimAtFirst { get; set; }
        [Required]
        [ForeignKey("NewSim")]
        public int SimNew { get; set; }


        [JsonIgnore]
        public virtual Employee CreateByEmployee { get; set; }
        [JsonIgnore]
        public virtual Gps Gps { get; set; }
        [JsonIgnore]
        public virtual Sim OldSim { get; set; }
        [JsonIgnore]
        public virtual Sim NewSim { get; set; }
    }
}