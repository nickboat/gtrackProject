using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace gtrackProject.Models.product
{
    public class GpsState
    {
        public GpsState()
        {
            Gpses = new List<Gps>();
        }
        [Key]
        public byte Id { get; set; }
        public string StatusNameTh { get; set; }
        public string StatusNameEn { get; set; }



        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<Gps> Gpses { get; set; }
    }
}
