using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using gtrackProject.Models.vehicle;

namespace gtrackProject.Models.Mapping.vehicle
{
    public class VehicleHeadTypeMap : EntityTypeConfiguration<VehicleHeadType>
    {
        public VehicleHeadTypeMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Name)
                .HasMaxLength(20);

            // Table & Column Mappings
            ToTable("vehicle_head_type");
            Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Name).HasColumnName("Name");
        }
    }
}
