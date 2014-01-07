using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gtrackProject.Models.vehicle
{
    public sealed class Province
    {
        public Province()
        {
            Vehicles = new List<Vehicle>();
        }

        public byte Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string ShortNameEn { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
