using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gtrackProject.Models.order
{
    public sealed class OrderExtendType
    {
        public OrderExtendType()
        {
            Orders = new List<Order>();
        }

        public byte Id { get; set; }
        [Required]
        public string TypeName { get; set; }
        [Required, Range(1,99)]
        public byte Value { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
