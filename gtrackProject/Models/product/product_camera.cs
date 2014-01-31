using System.ComponentModel.DataAnnotations;

namespace gtrackProject.Models.product
{
    public sealed class ProductCamera
    {
        public int Id { get; set; }
        [Required]
        public string Serial { get; set; }
        public int? ProductId { get; set; }
        public byte? Status { get; set; }
        public byte? State { get; set; }
        public ProductGps ProductGps { get; set; }
        public ProductCameraStatus CameraStatus { get; set; }
        public ProductGpsProcessState CameraState { get; set; }
    }
}
