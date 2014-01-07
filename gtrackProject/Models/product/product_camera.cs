namespace gtrackProject.Models.product
{
    public class ProductCamera
    {
        public int Id { get; set; }
        public string Serial { get; set; }
        public int? ProductId { get; set; }
        public virtual ProductGps ProductGps { get; set; }
    }
}
