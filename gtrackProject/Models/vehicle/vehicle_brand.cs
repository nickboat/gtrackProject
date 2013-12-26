using System;
using System.Collections.Generic;

namespace gtrackProject.Models
{
    public sealed partial class VehicleBrand
    {
        public VehicleBrand()
        {
            this.VehicleModels = new List<VehicleModel>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }
        public ICollection<VehicleModel> VehicleModels { get; set; }
    }
}
