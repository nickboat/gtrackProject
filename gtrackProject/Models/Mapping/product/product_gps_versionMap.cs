using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace gtrackProject.Models.Mapping
{
    public class ProductGpsVersionMap : EntityTypeConfiguration<ProductGpsVersion>
    {
        public ProductGpsVersionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("product_gps_version");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
        }
    }
}
