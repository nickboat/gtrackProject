using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace gtrackProject.Models.vehicle
{
    public class LpType
    {
        public LpType()
        {
            Vehicles = new List<Vehicle>();
        }
        [Key]
        public byte Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Meaning { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
