using System.Data.Entity.ModelConfiguration;
using gtrackProject.Models.product;

namespace gtrackProject.Models.Mapping.product
{
    public class ProductGpsVersionMap : EntityTypeConfiguration<ProductGpsVersion>
    {
        public ProductGpsVersionMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Name)
                .HasMaxLength(20);

            // Table & Column Mappings
            ToTable("product_gps_version");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Name).HasColumnName("Name");
        }
    }
}
