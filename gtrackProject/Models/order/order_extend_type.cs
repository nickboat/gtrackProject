using System;
using System.Collections.Generic;

namespace gtrackProject.Models
{
    public sealed partial class OrderExtendType
    {
        public OrderExtendType()
        {
            this.Orders = new List<Order>();
        }

        public byte Id { get; set; }
        public string TypeName { get; set; }
        public byte Value { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
