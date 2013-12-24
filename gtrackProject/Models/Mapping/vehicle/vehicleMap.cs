using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace gtrackProject.Models.Mapping
{
    public class vehicleMap : EntityTypeConfiguration<vehicle>
    {
        public vehicleMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.IdCar)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(6);

            this.Property(t => t.LicensePlate)
                .HasMaxLength(8);

            this.Property(t => t.BodyNo)
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("vehicles");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdCar).HasColumnName("IdCar");
            this.Property(t => t.LicensePlate).HasColumnName("LicensePlate");
            this.Property(t => t.LicensePlate_Type).HasColumnName("LicensePlate_Type");
            this.Property(t => t.LicensePlate_At).HasColumnName("LicensePlate_At");
            this.Property(t => t.ModelCar_Id).HasColumnName("ModelCar_Id");
            this.Property(t => t.ColorCar_Id).HasColumnName("ColorCar_Id");
            this.Property(t => t.OganizeCar_Id).HasColumnName("OganizeCar_Id");
            this.Property(t => t.BodyNo).HasColumnName("BodyNo");
            this.Property(t => t.HD_Id).HasColumnName("HD_Id");

            // Relationships
            this.HasRequired(t => t.hd)
                .WithMany(t => t.vehicles)
                .HasForeignKey(d => d.HD_Id);
            this.HasOptional(t => t.lp_type)
                .WithMany(t => t.vehicles)
                .HasForeignKey(d => d.LicensePlate_Type);
            this.HasOptional(t => t.province)
                .WithMany(t => t.vehicles)
                .HasForeignKey(d => d.LicensePlate_At);
            this.HasOptional(t => t.vehicle_color)
                .WithMany(t => t.vehicles)
                .HasForeignKey(d => d.ColorCar_Id);
            this.HasOptional(t => t.vehicle_model)
                .WithMany(t => t.vehicles)
                .HasForeignKey(d => d.ModelCar_Id);
            this.HasOptional(t => t.vehicle_oganize)
                .WithMany(t => t.vehicles)
                .HasForeignKey(d => d.OganizeCar_Id);

        }
    }
}
