using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace gtrackProject.Models.product
{
    public class SimBrand
    {
        public SimBrand()
        {
            ProductGpss = new List<ProductGps>();
        }
        [Key]
        public byte Id { get; set; }
        [Required]
        public string BrandName { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<ProductGps> ProductGpss { get; set; }
    }
}
