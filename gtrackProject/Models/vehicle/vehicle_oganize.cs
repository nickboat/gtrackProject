using System;
using System.Collections.Generic;

namespace gtrackProject.Models
{
    public sealed partial class VehicleOganize
    {
        public VehicleOganize()
        {
            this.Vehicles = new List<Vehicle>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
