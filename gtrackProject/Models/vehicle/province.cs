using System;
using System.Collections.Generic;

namespace gtrackProject.Models
{
    public sealed partial class Province
    {
        public Province()
        {
            this.Vehicles = new List<Vehicle>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string ShortNameEn { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
