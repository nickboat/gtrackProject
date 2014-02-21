using System;
using System.ComponentModel.DataAnnotations;
using gtrackProject.Models.product;
using gtrackProject.Models.vehicle;
using Newtonsoft.Json;

namespace gtrackProject.Models.driver
{
    public class LogCardReader
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime AtDateTime { get; set; }
        [Required]
        public int DriverId { get; set; }
        [Required]
        public int ProductGpsId { get; set; }
        [Required]
        public int VehicleId { get; set; }


        [JsonIgnore]
        public virtual Driver Driver { get; set; }
        [JsonIgnore]
        public virtual ProductGps ProductGps { get; set; }
        [JsonIgnore]
        public virtual Vehicle Vehicle { get; set; }
    }
}