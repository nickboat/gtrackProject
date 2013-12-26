using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace gtrackProject.Models.Mapping
{
    public class ProductGpsTypeMap : EntityTypeConfiguration<ProductGpsType>
    {
        public ProductGpsTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.StatusNameTh)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.StatusNameEn)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("product_gps_type");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.StatusNameTh).HasColumnName("StatusName_TH");
            this.Property(t => t.StatusNameEn).HasColumnName("StatusName_EN");
        }
    }
}
