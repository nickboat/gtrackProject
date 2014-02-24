using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace gtrackProject.Models.order
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderState
    {
        public OrderState()
        {
            FixOrders = new List<FixOrder>();
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
        public ICollection<FixOrder> FixOrders { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<Order> Orders { get; set; }
    }
}
