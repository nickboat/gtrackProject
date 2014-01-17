using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gtrackProject.Models.driver
{
    public sealed class DriverCategory
    {
        public DriverCategory()
        {
            Drivers = new List<Driver>();
        }

        public byte Id { get; set; }
        [Required] public short Value { get; set; }
        [Required] public string Name { get; set; }
        public ICollection<Driver> Drivers { get; set; }
    }
}
