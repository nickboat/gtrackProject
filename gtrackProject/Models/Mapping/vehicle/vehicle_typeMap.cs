using System.Data.Entity.ModelConfiguration;
using gtrackProject.Models.vehicle;

namespace gtrackProject.Models.Mapping.vehicle
{
    public class VehicleTypeMap : EntityTypeConfiguration<VehicleType>
    {
        public VehicleTypeMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            ToTable("vehicle_type");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.HeadId).HasColumnName("Head_Id");

            // Relationships
            HasRequired(t => t.VehicleHeadType)
                .WithMany(t => t.VehicleTypes)
                .HasForeignKey(d => d.HeadId);

        }
    }
}
