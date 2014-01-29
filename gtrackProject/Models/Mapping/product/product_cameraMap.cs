using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using gtrackProject.Models.product;

namespace gtrackProject.Models.Mapping.product
{
    public class ProductCameraMap : EntityTypeConfiguration<ProductCamera>
    {
        public ProductCameraMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Serial)
                .IsRequired()
                .HasMaxLength(20);
            
            // Table & Column Mappings
            ToTable("product_camera");
            Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Serial).HasColumnName("Serial");
            Property(t => t.ProductId).HasColumnName("Product_Id");
            Property(t => t.Status).HasColumnName("Status");

            // Relationships
            HasRequired(t => t.ProductGps)
                .WithMany(t => t.Cameras)
                .HasForeignKey(d => d.ProductId);

            HasRequired(t => t.CameraStatus)
                .WithMany(t => t.Cameras)
                .HasForeignKey(d => d.Status);
        }
    }
}
