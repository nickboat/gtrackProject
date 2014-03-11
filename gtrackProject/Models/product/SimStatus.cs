using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace gtrackProject.Models.product
{
    public class SimStatus
    {
        public SimStatus()
        {
            Sims = new List<Sim>();
        }

        [Key]
        public byte Id { get; set; }
        [Required]
        public string Name { get; set; }


        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<Sim> Sims { get; set; }
    }
}