using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace gtrackProject.Models.driver
{
    public class DriverCategory
    {
        public DriverCategory()
        {
            Drivers = new List<Driver>();
        }
        [Key]
        public byte Id { get; set; }
        [Required] public short Value { get; set; }
        [Required] public string Name { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<Driver> Drivers { get; set; }
    }
}
