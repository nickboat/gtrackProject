using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace gtrackProject.Models.vehicle
{
    public class VehicleType
    {
        public VehicleType()
        {
            VehicleModels = new List<VehicleModel>();
        }
        [Key]
        public byte Id { get; set; }
        [Required]
        public string Name { get; set; }
        [ForeignKey("VehicleHeadType")]
        public byte? HeadId { get; set; }
        [JsonIgnore]// hide relation object in Get/GetAll
        //[IgnoreDataMember] //show relation object when query on url
        public virtual VehicleHeadType VehicleHeadType { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember] //show relation object when query on url
        public ICollection<VehicleModel> VehicleModels { get; set; }
    }
}
