using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using gtrackProject.Models.account;
using gtrackProject.Models.vehicle;
using Newtonsoft.Json;

namespace gtrackProject.Models.product
{
    public class LogSwap
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("CreateByEmployee")]
        public int CreateBy { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        [ForeignKey("Gps1")]
        public int GpsA { get; set; }
        [Required]
        [ForeignKey("Gps2")]
        public int GpsB { get; set; }
        [Required]
        [ForeignKey("Vehicle1")]
        public int VehicleA { get; set; }
        [Required]
        [ForeignKey("Vehicle2")]
        public int VehicleB { get; set; }
        [Required]
        [ForeignKey("LogStatus")]
        public int Status { get; set; }
        public string Comment { get; set; }



        [JsonIgnore]
        public virtual Employee CreateByEmployee { get; set; }
        [JsonIgnore]
        public virtual Gps Gps1 { get; set; }
        [JsonIgnore]
        public virtual Gps Gps2 { get; set; }
        [JsonIgnore]
        public virtual LogStatus LogStatus { get; set; }
        [JsonIgnore]
        public virtual Vehicle Vehicle1 { get; set; }
        [JsonIgnore]
        public virtual Vehicle Vehicle2 { get; set; }
    }
}