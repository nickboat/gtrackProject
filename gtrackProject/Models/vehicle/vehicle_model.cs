using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace gtrackProject.Models.vehicle
{
    public sealed class VehicleModel
    {
        public VehicleModel()
        {
            Vehicles = new List<Vehicle>();
        }

        public short Id { get; set; }
        [Required]
        public byte BrandId { get; set; }
        [Required]
        public string Name { get; set; }
        public short? Year { get; set; }
        [Required]
        public byte TypeId { get; set; }
        public VehicleBrand VehicleBrand { get; set; }
        public VehicleType VehicleType { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
