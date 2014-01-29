using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using gtrackProject.Models.vehicle;

namespace gtrackProject.Models.Mapping.vehicle
{
    public class LpTypeMap : EntityTypeConfiguration<LpType>
    {
        public LpTypeMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Name)
                .HasMaxLength(20);

            Property(t => t.Meaning)
                .HasMaxLength(255);

            // Table & Column Mappings
            ToTable("lp_type");
            Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.Meaning).HasColumnName("Meaning");
        }
    }
}
