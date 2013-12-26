using System;
using System.Collections.Generic;

namespace gtrackProject.Models
{
    public sealed partial class VehicleModel
    {
        public VehicleModel()
        {
            this.Vehicles = new List<Vehicle>();
        }

        public short Id { get; set; }
        public byte BrandId { get; set; }
        public string Name { get; set; }
        public short? Year { get; set; }
        public byte TypeId { get; set; }
        public VehicleBrand VehicleBrand { get; set; }
        public VehicleType VehicleType { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
