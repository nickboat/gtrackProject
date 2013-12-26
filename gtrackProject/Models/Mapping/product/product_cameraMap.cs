using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace gtrackProject.Models.Mapping
{
    public class ProductCameraMap : EntityTypeConfiguration<ProductCamera>
    {
        public ProductCameraMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Serial)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("product_camera");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Serial).HasColumnName("Serial");
            this.Property(t => t.ProductId).HasColumnName("Product_Id");

            // Relationships
            this.HasRequired(t => t.ProductGps)
                .WithMany(t => t.Cameras)
                .HasForeignKey(d => d.ProductId);

        }
    }
}
