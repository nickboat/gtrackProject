using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using gtrackProject.Models.universe;

namespace gtrackProject.Models.Mapping
{
    public class UniverseMap : EntityTypeConfiguration<Universe>
    {
        public UniverseMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.CmCommand)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.CmEngine)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.CmMeter)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.CmBatt)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.CmTemp)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.CmGps)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.CmSignalStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.IpGps)
                .HasMaxLength(21);

            // Table & Column Mappings
            this.ToTable("universe");
            this.Property(t => t.VehicleId).HasColumnName("Vehicle_Id");
            this.Property(t => t.GpsProductId).HasColumnName("GPS_Product_Id");
            this.Property(t => t.CurrentDataDatetime).HasColumnName("CurrentData_Datetime");
            this.Property(t => t.CorrectDataId).HasColumnName("CorrectData_Id");
            this.Property(t => t.CorrectDataDatetime).HasColumnName("CorrectData_Datetime");
            this.Property(t => t.CmCommand).HasColumnName("CM_Command");
            this.Property(t => t.CmEngine).HasColumnName("CM_Engine");
            this.Property(t => t.CmMeter).HasColumnName("CM_Meter");
            this.Property(t => t.CmBatt).HasColumnName("CM_Batt");
            this.Property(t => t.FuelLevel).HasColumnName("FuelLevel");
            this.Property(t => t.CmTemp).HasColumnName("CM_Temp");
            this.Property(t => t.TempLevel).HasColumnName("TempLevel");
            this.Property(t => t.CmGps).HasColumnName("CM_GPS");
            this.Property(t => t.CmSignalStatus).HasColumnName("CM_SignalStatus");
            this.Property(t => t.Speed).HasColumnName("Speed");
            this.Property(t => t.Direction).HasColumnName("Direction");
            this.Property(t => t.IpGps).HasColumnName("IpGPS");
            this.Property(t => t.Port).HasColumnName("Port");
            this.Property(t => t.LaGoogle).HasColumnName("LaGoogle");
            this.Property(t => t.LongGoogle).HasColumnName("LongGoogle");
            this.Property(t => t.DisplayStatus).HasColumnName("Display_Status");
            this.Property(t => t.DriverId).HasColumnName("Driver_Id");
            this.Property(t => t.OrderId).HasColumnName("Order_Id");
            this.Property(t => t.FixOrderId).HasColumnName("FixOrder_Id");
            this.Property(t => t.Id).HasColumnName("Id");

            // Relationships
            this.HasOptional(t => t.Driver)
                .WithMany(t => t.Universes)
                .HasForeignKey(d => d.DriverId);
            this.HasOptional(t => t.FixOrders)
                .WithMany(t => t.Universes)
                .HasForeignKey(d => d.FixOrderId);
            this.HasOptional(t => t.Order)
                .WithMany(t => t.Universes)
                .HasForeignKey(d => d.OrderId);
            this.HasOptional(t => t.ProductGps)
                .WithMany(t => t.Universes)
                .HasForeignKey(d => d.GpsProductId);
            this.HasOptional(t => t.UnCmBatt)
                .WithMany(t => t.Universes)
                .HasForeignKey(d => d.CmBatt);
            this.HasOptional(t => t.UnCmComm)
                .WithMany(t => t.Universes)
                .HasForeignKey(d => d.CmCommand);
            this.HasOptional(t => t.UnCmEngine)
                .WithMany(t => t.Universes)
                .HasForeignKey(d => d.CmEngine);
            this.HasOptional(t => t.UnCmGps)
                .WithMany(t => t.Universes)
                .HasForeignKey(d => d.CmGps);
            this.HasOptional(t => t.UnCmMeter)
                .WithMany(t => t.Universes)
                .HasForeignKey(d => d.CmMeter);
            this.HasRequired(t => t.UnCmSignal)
                .WithMany(t => t.Universes)
                .HasForeignKey(d => d.CmSignalStatus);
            this.HasOptional(t => t.UnCmTemp)
                .WithMany(t => t.Universes)
                .HasForeignKey(d => d.CmTemp);
            this.HasRequired(t => t.UnDisplayStatus)
                .WithMany(t => t.Universes)
                .HasForeignKey(d => d.DisplayStatus);
            this.HasRequired(t => t.Vehicle)
                .WithMany(t => t.Universes)
                .HasForeignKey(d => d.VehicleId);

        }
    }
}
