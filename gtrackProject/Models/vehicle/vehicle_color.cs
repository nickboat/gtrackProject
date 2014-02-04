using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace gtrackProject.Models.vehicle
{
    public class VehicleColor
    {
        public VehicleColor()
        {
            Vehicles = new List<Vehicle>();
        }
        [Key]
        public byte Id { get; set; }
        [Required]
        public string Name { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
