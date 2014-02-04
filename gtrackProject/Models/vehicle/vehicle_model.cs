using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace gtrackProject.Models.vehicle
{
    public class VehicleModel
    {
        public VehicleModel()
        {
            Vehicles = new List<Vehicle>();
        }

        public short Id { get; set; }
        [Required]
        [ForeignKey("VehicleBrand")]
        public byte BrandId { get; set; }
        [Required]
        public string Name { get; set; }
        public short? Year { get; set; }
        [Required]
        [ForeignKey("VehicleType")]
        public byte TypeId { get; set; }
        [JsonIgnore]
        public virtual VehicleBrand VehicleBrand { get; set; }
        [JsonIgnore]
        public virtual VehicleType VehicleType { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
