using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

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
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Driver> Drivers { get; set; }
    }
}
