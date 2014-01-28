using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace gtrackProject.Models.product
{
    public sealed class SimPaymentType
    {
        public SimPaymentType()
        {
            ProductGpss = new List<ProductGps>();
        }

        public byte Id { get; set; }
        public string PaymentName { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<ProductGps> ProductGpss { get; set; }
    }
}
