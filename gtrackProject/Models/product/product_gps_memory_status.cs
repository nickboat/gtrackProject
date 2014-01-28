using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace gtrackProject.Models.product
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ProductGpsMemoryStatus
    {
        public ProductGpsMemoryStatus()
        {
            ProductGpss = new List<ProductGps>();
        }

        public byte Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Val { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<ProductGps> ProductGpss { get; set; }
    }
}