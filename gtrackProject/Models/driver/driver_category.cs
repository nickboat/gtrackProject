using System;
using System.Collections.Generic;

namespace gtrackProject.Models
{
    public sealed partial class DriverCategory
    {
        public DriverCategory()
        {
            this.Drivers = new List<Driver>();
        }

        public byte Id { get; set; }
        public short Value { get; set; }
        public string Name { get; set; }
        public ICollection<Driver> Drivers { get; set; }
    }
}
