using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace gtrackProject.Models.Mapping
{
    public class product_gps_typeMap : EntityTypeConfiguration<product_gps_type>
    {
        public product_gps_typeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.StatusName_TH)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.StatusName_EN)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("product_gps_type");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.StatusName_TH).HasColumnName("StatusName_TH");
            this.Property(t => t.StatusName_EN).HasColumnName("StatusName_EN");
        }
    }
}
