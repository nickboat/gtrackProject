using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace gtrackProject.Models.Mapping
{
    public class vehicle_modelMap : EntityTypeConfiguration<vehicle_model>
    {
        public vehicle_modelMap()
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
            this.Property(t => t.Brand_Id).HasColumnName("Brand_Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Year).HasColumnName("Year");
            this.Property(t => t.Type_Id).HasColumnName("Type_Id");

            // Relationships
            this.HasRequired(t => t.vehicle_brand)
                .WithMany(t => t.vehicle_model)
                .HasForeignKey(d => d.Brand_Id);
            this.HasRequired(t => t.vehicle_type)
                .WithMany(t => t.vehicle_model)
                .HasForeignKey(d => d.Type_Id);

        }
    }
}
