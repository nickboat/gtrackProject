using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace gtrackProject.Models.product
{
    public class ProductCamera
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Serial { get; set; }
        public int? ProductId { get; set; }
        public byte? Status { get; set; }
        public byte? State { get; set; }
        [JsonIgnore]
        public virtual ProductGps ProductGps { get; set; }
        [JsonIgnore]
        public virtual ProductCameraStatus CameraStatus { get; set; }
        [JsonIgnore]
        public virtual ProductProcessState CameraState { get; set; }
    }
}
