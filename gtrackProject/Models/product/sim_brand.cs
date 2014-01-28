using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace gtrackProject.Models.product
{
    public sealed class SimBrand
    {
        public SimBrand()
        {
            ProductGpss = new List<ProductGps>();
        }

        public byte Id { get; set; }
        public string BrandName { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<ProductGps> ProductGpss { get; set; }
    }
}
