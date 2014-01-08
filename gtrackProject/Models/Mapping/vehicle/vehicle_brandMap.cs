using System.Data.Entity.ModelConfiguration;
using gtrackProject.Models.vehicle;

namespace gtrackProject.Models.Mapping.vehicle
{
    public class VehicleBrandMap : EntityTypeConfiguration<VehicleBrand>
    {
        public VehicleBrandMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            ToTable("vehicle_brand");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Name).HasColumnName("Name");
        }
    }
}
