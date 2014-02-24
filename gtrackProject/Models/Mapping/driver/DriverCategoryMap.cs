using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using gtrackProject.Models.driver;

namespace gtrackProject.Models.Mapping.driver
{
    public class DriverCategoryMap : EntityTypeConfiguration<DriverCategory>
    {
        public DriverCategoryMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("driver_category");
            Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Value).HasColumnName("Value");
            Property(t => t.Name).HasColumnName("Name");
        }
    }
}
