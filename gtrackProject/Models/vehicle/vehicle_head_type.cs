using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gtrackProject.Models.vehicle
{
    public sealed class VehicleHeadType
    {
        public VehicleHeadType()
        {
            VehicleTypes = new List<VehicleType>();
        }

        public byte Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<VehicleType> VehicleTypes { get; set; }
    }
}
