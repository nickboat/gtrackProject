using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace gtrackProject.Models.Mapping
{
    public class VehicleModelMap : EntityTypeConfiguration<VehicleModel>
    {
        public VehicleModelMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("vehicle_model");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.BrandId).HasColumnName("Brand_Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Year).HasColumnName("Year");
            this.Property(t => t.TypeId).HasColumnName("Type_Id");

            // Relationships
            this.HasRequired(t => t.VehicleBrand)
                .WithMany(t => t.VehicleModels)
                .HasForeignKey(d => d.BrandId);
            this.HasRequired(t => t.VehicleType)
                .WithMany(t => t.VehicleModels)
                .HasForeignKey(d => d.TypeId);

        }
    }
}
