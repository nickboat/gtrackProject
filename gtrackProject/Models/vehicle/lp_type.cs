using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gtrackProject.Models.vehicle
{
    public sealed class LpType
    {
        public LpType()
        {
            Vehicles = new List<Vehicle>();
        }

        public byte Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Meaning { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
