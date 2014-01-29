using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using gtrackProject.Models.vehicle;

namespace gtrackProject.Models.Mapping.vehicle
{
    public class VehicleOganizeMap : EntityTypeConfiguration<VehicleOganize>
    {
        public VehicleOganizeMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("vehicle_oganize");
            Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Name).HasColumnName("Name");
        }
    }
}
