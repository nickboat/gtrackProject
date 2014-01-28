using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gtrackProject.Models.product
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ProductCameraStatus
    {
        public ProductCameraStatus()
        {
            Cameras = new List<ProductCamera>();
        }

        public byte Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Val { get; set; }
        public ICollection<ProductCamera> Cameras { get; set; }

    }
}