using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

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
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Order> Orders { get; set; }
    }
}
