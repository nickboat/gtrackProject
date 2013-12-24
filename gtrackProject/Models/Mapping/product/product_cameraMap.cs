using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace gtrackProject.Models.Mapping
{
    public class product_cameraMap : EntityTypeConfiguration<product_camera>
    {
        public product_cameraMap()
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
            this.Property(t => t.Product_Id).HasColumnName("Product_Id");

            // Relationships
            this.HasRequired(t => t.product_gps)
                .WithMany(t => t.product_camera)
                .HasForeignKey(d => d.Product_Id);

        }
    }
}
