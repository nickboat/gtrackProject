using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace gtrackProject.Models.order
{
    public class OrderExtendType
    {
        public OrderExtendType()
        {
            Orders = new List<Order>();
        }
        [Key]
        public byte Id { get; set; }
        [Required]
        public string TypeName { get; set; }
        [Required, Range(1,99)]
        public byte Value { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<Order> Orders { get; set; }
    }
}
