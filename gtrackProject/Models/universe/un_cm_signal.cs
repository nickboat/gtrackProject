using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gtrackProject.Models.universe
{
    public sealed class UnCmSignal
    {
        public UnCmSignal()
        {
            Universes = new List<Universe>();
        }
        [Required, StringLength(1)]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string MsgTh { get; set; }
        public string MsgEn { get; set; }
        public ICollection<Universe> Universes { get; set; }
    }
}
