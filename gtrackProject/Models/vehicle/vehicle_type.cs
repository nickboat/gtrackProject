using System.Collections.Generic;

namespace gtrackProject.Models.vehicle
{
    public sealed class VehicleType
    {
        public VehicleType()
        {
            VehicleModels = new List<VehicleModel>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }
        public byte HeadId { get; set; }
        public VehicleHeadType VehicleHeadType { get; set; }
        public ICollection<VehicleModel> VehicleModels { get; set; }
    }
}
