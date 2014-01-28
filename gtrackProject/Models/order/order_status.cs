using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

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
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<FixOrders> FixOrders { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Order> Orders { get; set; }
    }
}
