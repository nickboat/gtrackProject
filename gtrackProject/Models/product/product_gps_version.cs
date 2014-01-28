using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using gtrackProject.Models.order;
using Newtonsoft.Json;

namespace gtrackProject.Models.product
{
    public sealed class ProductGpsVersion
    {
        public ProductGpsVersion()
        {
            Orders = new List<Order>();
            ProductGpss = new List<ProductGps>();
        }

        public byte Id { get; set; }
        [Required]
        public string Name { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Order> Orders { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<ProductGps> ProductGpss { get; set; }
    }
}
