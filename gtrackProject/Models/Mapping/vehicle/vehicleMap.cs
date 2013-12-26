using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace gtrackProject.Models.Mapping
{
    public class VehicleMap : EntityTypeConfiguration<Vehicle>
    {
        public VehicleMap()
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
            this.Property(t => t.LicensePlateType).HasColumnName("LicensePlate_Type");
            this.Property(t => t.LicensePlateAt).HasColumnName("LicensePlate_At");
            this.Property(t => t.ModelCarId).HasColumnName("ModelCar_Id");
            this.Property(t => t.ColorCarId).HasColumnName("ColorCar_Id");
            this.Property(t => t.OganizeCarId).HasColumnName("OganizeCar_Id");
            this.Property(t => t.BodyNo).HasColumnName("BodyNo");
            this.Property(t => t.HdId).HasColumnName("HD_Id");

            // Relationships
            this.HasRequired(t => t.Hd)
                .WithMany(t => t.Vehicles)
                .HasForeignKey(d => d.HdId);
            this.HasOptional(t => t.LpType)
                .WithMany(t => t.Vehicles)
                .HasForeignKey(d => d.LicensePlateType);
            this.HasOptional(t => t.Province)
                .WithMany(t => t.Vehicles)
                .HasForeignKey(d => d.LicensePlateAt);
            this.HasOptional(t => t.VehicleColor)
                .WithMany(t => t.Vehicles)
                .HasForeignKey(d => d.ColorCarId);
            this.HasOptional(t => t.VehicleModel)
                .WithMany(t => t.Vehicles)
                .HasForeignKey(d => d.ModelCarId);
            this.HasOptional(t => t.VehicleOganize)
                .WithMany(t => t.Vehicles)
                .HasForeignKey(d => d.OganizeCarId);

        }
    }
}
