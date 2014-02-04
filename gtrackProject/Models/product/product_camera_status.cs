using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace gtrackProject.Models.product
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductCameraStatus
    {
        public ProductCameraStatus()
        {
            Cameras = new List<ProductCamera>();
        }
        [Key]
        public byte Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Val { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<ProductCamera> Cameras { get; set; }

    }
}