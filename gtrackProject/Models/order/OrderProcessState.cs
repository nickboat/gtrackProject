using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace gtrackProject.Models.order
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderProcessState
    {
        public OrderProcessState()
        {
            FixOrders = new List<FixOrders>();
            Orders = new List<Order>();
        }
        [Key]
        public byte Id { get; set; }
        [Required]
        public string StatusTh { get; set; }
        [Required]
        public string StatusEn { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<FixOrders> FixOrders { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<Order> Orders { get; set; }
    }
}
