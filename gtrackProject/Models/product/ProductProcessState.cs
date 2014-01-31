using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace gtrackProject.Models.product
{
    public sealed class ProductProcessState
    {
        public ProductProcessState()
        {
            ProductGpss = new List<ProductGps>();
            Cameras = new List<ProductCamera>();
        }

        public byte Id { get; set; }
        public string StatusNameTh { get; set; }
        public string StatusNameEn { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<ProductGps> ProductGpss { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<ProductCamera> Cameras { get; set; }
    }
}
