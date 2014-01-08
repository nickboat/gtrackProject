using System.Data.Entity.ModelConfiguration;
using gtrackProject.Models.vehicle;

namespace gtrackProject.Models.Mapping.vehicle
{
    public class VehicleModelMap : EntityTypeConfiguration<VehicleModel>
    {
        public VehicleModelMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("vehicle_model");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.BrandId).HasColumnName("Brand_Id");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.Year).HasColumnName("Year");
            Property(t => t.TypeId).HasColumnName("Type_Id");

            // Relationships
            HasRequired(t => t.VehicleBrand)
                .WithMany(t => t.VehicleModels)
                .HasForeignKey(d => d.BrandId);
            HasRequired(t => t.VehicleType)
                .WithMany(t => t.VehicleModels)
                .HasForeignKey(d => d.TypeId);

        }
    }
}
