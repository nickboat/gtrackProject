using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace gtrackProject.Models.vehicle
{
    public sealed class VehicleType
    {
        public VehicleType()
        {
            VehicleModels = new List<VehicleModel>();
        }

        public byte Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public byte HeadId { get; set; }
        public VehicleHeadType VehicleHeadType { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<VehicleModel> VehicleModels { get; set; }
    }
}
