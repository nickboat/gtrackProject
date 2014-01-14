using System.Data.Entity.ModelConfiguration;
using gtrackProject.Models.vehicle;

namespace gtrackProject.Models.Mapping.vehicle
{
    public class VehicleMap : EntityTypeConfiguration<Vehicle>
    {
        public VehicleMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.IdCar)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(6);

            Property(t => t.LicensePlate)
                .HasMaxLength(8);

            Property(t => t.BodyNo)
                .HasMaxLength(30);

            // Table & Column Mappings
            ToTable("vehicles");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.IdCar).HasColumnName("IdCar");
            Property(t => t.LicensePlate).HasColumnName("LicensePlate");
            Property(t => t.LicensePlateType).HasColumnName("LicensePlate_Type");
            Property(t => t.LicensePlateAt).HasColumnName("LicensePlate_At");
            Property(t => t.ModelCarId).HasColumnName("ModelCar_Id");
            Property(t => t.ColorCarId).HasColumnName("ColorCar_Id");
            Property(t => t.OganizeCarId).HasColumnName("OganizeCar_Id");
            Property(t => t.BodyNo).HasColumnName("BodyNo");
            Property(t => t.HdId).HasColumnName("HD_Id");

            // Relationships
            HasRequired(t => t.Hd)
                .WithMany(t => t.Vehicles)
                .HasForeignKey(d => d.HdId);
            HasOptional(t => t.LpType)
                .WithMany(t => t.Vehicles)
                .HasForeignKey(d => d.LicensePlateType);
            HasOptional(t => t.Province)
                .WithMany(t => t.Vehicles)
                .HasForeignKey(d => d.LicensePlateAt);
            HasOptional(t => t.VehicleColor)
                .WithMany(t => t.Vehicles)
                .HasForeignKey(d => d.ColorCarId);
            HasOptional(t => t.VehicleModel)
                .WithMany(t => t.Vehicles)
                .HasForeignKey(d => d.ModelCarId);
            HasOptional(t => t.VehicleOganize)
                .WithMany(t => t.Vehicles)
                .HasForeignKey(d => d.OganizeCarId);

        }
    }
}
