using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public ICollection<Universe> Universes { get; set; }
    }
}
