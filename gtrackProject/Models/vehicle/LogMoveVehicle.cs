using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using gtrackProject.Models.account;
using Newtonsoft.Json;

namespace gtrackProject.Models.vehicle
{
    public class LogMoveVehicle
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }
        [Required]
        [ForeignKey("CreateByEmployee")]
        public int CreateBy { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        [ForeignKey("FirstHd")]
        public int HdAtFirst { get; set; }
        [Required]
        public string IdCarAtFirst { get; set; }
        [Required]
        [ForeignKey("MoveHd")]
        public int HdMoveTo { get; set; }
        [Required]
        public string IdCarMoveTo { get; set; }
        public string Comment { get; set; }


        [JsonIgnore]
        public virtual Employee CreateByEmployee { get; set; }
        [JsonIgnore]
        public virtual Vehicle Vehicle { get; set; }
        [JsonIgnore]
        public virtual Hd FirstHd { get; set; }
        [JsonIgnore]
        public virtual Hd MoveHd { get; set; }
    }
}