using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace gtrackProject.Models.Mapping
{
    public class vehicle_typeMap : EntityTypeConfiguration<vehicle_type>
    {
        public vehicle_typeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("vehicle_type");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Head_Id).HasColumnName("Head_Id");

            // Relationships
            this.HasRequired(t => t.vehicle_head_type)
                .WithMany(t => t.vehicle_type)
                .HasForeignKey(d => d.Head_Id);

        }
    }
}
