using System;
using System.Collections.Generic;

namespace gtrackProject.Models
{
    public sealed partial class VehicleHeadType
    {
        public VehicleHeadType()
        {
            this.VehicleTypes = new List<VehicleType>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }
        public ICollection<VehicleType> VehicleTypes { get; set; }
    }
}
