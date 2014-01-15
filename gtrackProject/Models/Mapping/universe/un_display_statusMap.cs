using System.Data.Entity.ModelConfiguration;
using gtrackProject.Models.universe;

namespace gtrackProject.Models.Mapping.universe
{
    public class UnDisplayStatusMap : EntityTypeConfiguration<UnDisplayStatus>
    {
        public UnDisplayStatusMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Name)
                .HasMaxLength(20);

            // Table & Column Mappings
            ToTable("un_display_status");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Name).HasColumnName("Name");
        }
    }
}
