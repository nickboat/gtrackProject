using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using gtrackProject.Models.product;

namespace gtrackProject.Models.Mapping.product
{
    public class GpsStateMap : EntityTypeConfiguration<GpsState>
    {
        public GpsStateMap()
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
            ToTable("product_process_state");
            Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.StatusNameTh).HasColumnName("StatusName_TH");
            Property(t => t.StatusNameEn).HasColumnName("StatusName_EN");
        }
    }
}
