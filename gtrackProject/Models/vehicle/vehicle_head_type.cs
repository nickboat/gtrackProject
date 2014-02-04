using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace gtrackProject.Models.vehicle
{
    public class VehicleHeadType
    {
        public VehicleHeadType()
        {
            VehicleTypes = new List<VehicleType>();
        }

        public byte Id { get; set; }
        [Required]
        public string Name { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember] //show relation object when query on url
        public virtual ICollection<VehicleType> VehicleTypes { get; set; }
    }
}
