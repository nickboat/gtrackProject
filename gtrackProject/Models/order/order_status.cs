using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gtrackProject.Models.order
{
    public sealed class OrderStatus
    {
        public OrderStatus()
        {
            FixOrders = new List<FixOrders>();
            Orders = new List<Order>();
        }

        public byte Id { get; set; }
        [Required]
        public string StatusTh { get; set; }
        [Required]
        public string StatusEn { get; set; }
        public ICollection<FixOrders> FixOrders { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
