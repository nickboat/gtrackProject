using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gtrackProject.Models.vehicle
{
    public sealed class VehicleBrand
    {
        public VehicleBrand()
        {
            VehicleModels = new List<VehicleModel>();
        }

        public byte Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<VehicleModel> VehicleModels { get; set; }
    }
}
