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

        [Required, StringLength(1)]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<ProductCamera> Cameras { get; set; }

    }
}