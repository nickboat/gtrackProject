using System.Data.Entity.ModelConfiguration;
using gtrackProject.Models.product;

namespace gtrackProject.Models.Mapping.product
{
    public class ProductGpsTypeMap : EntityTypeConfiguration<ProductGpsType>
    {
        public ProductGpsTypeMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.StatusNameTh)
                .IsRequired()
                .HasMaxLength(15);

            Property(t => t.StatusNameEn)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            ToTable("product_gps_type");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.StatusNameTh).HasColumnName("StatusName_TH");
            Property(t => t.StatusNameEn).HasColumnName("StatusName_EN");
        }
    }
}
