using System.Data.Entity.ModelConfiguration;
using gtrackProject.Models.vehicle;

namespace gtrackProject.Models.Mapping.vehicle
{
    public class ProvinceMap : EntityTypeConfiguration<Province>
    {
        public ProvinceMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.ShortName)
                .IsFixedLength()
                .HasMaxLength(2);

            Property(t => t.ShortNameEn)
                .IsFixedLength()
                .HasMaxLength(3);

            // Table & Column Mappings
            ToTable("province");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.ShortName).HasColumnName("ShortName");
            Property(t => t.ShortNameEn).HasColumnName("ShortNameEN");
        }
    }
}
