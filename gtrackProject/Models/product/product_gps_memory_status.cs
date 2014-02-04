using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace gtrackProject.Models.product
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductGpsMemoryStatus
    {
        public ProductGpsMemoryStatus()
        {
            ProductGpss = new List<ProductGps>();
        }
        [Key]
        public byte Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Val { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<ProductGps> ProductGpss { get; set; }
    }
}