using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace gtrackProject.Models.product
{
    public class ProductProcessState
    {
        public ProductProcessState()
        {
            ProductGpss = new List<ProductGps>();
        }
        [Key]
        public byte Id { get; set; }
        public string StatusNameTh { get; set; }
        public string StatusNameEn { get; set; }



        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<ProductGps> ProductGpss { get; set; }
    }
}
