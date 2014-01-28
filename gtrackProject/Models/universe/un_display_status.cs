using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace gtrackProject.Models.universe
{
    public sealed class UnDisplayStatus
    {
        public UnDisplayStatus()
        {
            Universes = new List<Universe>();
        }

        public byte Id { get; set; }
        [Required]
        public string Name { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Universe> Universes { get; set; }
    }
}
