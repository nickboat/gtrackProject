using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace gtrackProject.Models.universe
{
    public class UnCmSignal
    {
        public UnCmSignal()
        {
            Universes = new List<Universe>();
        }
        [Key]
        [Required, StringLength(1)]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string MsgTh { get; set; }
        public string MsgEn { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<Universe> Universes { get; set; }
    }
}
