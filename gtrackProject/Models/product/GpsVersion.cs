using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using gtrackProject.Models.order;
using Newtonsoft.Json;

namespace gtrackProject.Models.product
{
    public class GpsVersion
    {
        public GpsVersion()
        {
            Orders = new List<Order>();
            Gpses = new List<Gps>();
        }
        [Key]
        public byte Id { get; set; }
        [Required]
        public string Name { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<Order> Orders { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<Gps> Gpses { get; set; }
    }
}
