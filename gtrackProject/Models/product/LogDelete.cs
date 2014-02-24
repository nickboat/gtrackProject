using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using gtrackProject.Models.account;
using gtrackProject.Models.vehicle;
using Newtonsoft.Json;

namespace gtrackProject.Models.product
{
    public class LogDelete
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("CreateByEmployee")]
        public int CreateBy { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        [ForeignKey("Gps")]
        public int GpsId { get; set; }
        [Required]
        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }
        [Required]
        [ForeignKey("LogStatus")]
        public int Status { get; set; }
        public string Comment { get; set; }


        [JsonIgnore]
        public virtual Employee CreateByEmployee { get; set; }
        [JsonIgnore]
        public virtual Gps Gps { get; set; }
        [JsonIgnore]
        public virtual Vehicle Vehicle { get; set; }
        [JsonIgnore]
        public virtual LogStatus LogStatus { get; set; }
    }
}