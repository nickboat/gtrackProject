using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using gtrackProject.Models.universe;

namespace gtrackProject.Models.Mapping.universe
{
    public class UniverseMap : EntityTypeConfiguration<Universe>
    {
        public UniverseMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.CmCommand)
                .IsFixedLength()
                .HasMaxLength(1);

            Property(t => t.CmEngine)
                .IsFixedLength()
                .HasMaxLength(1);

            Property(t => t.CmMeter)
                .IsFixedLength()
                .HasMaxLength(1);

            Property(t => t.CmBatt)
                .IsFixedLength()
                .HasMaxLength(1);

            Property(t => t.CmTemp)
                .IsFixedLength()
                .HasMaxLength(1);

            Property(t => t.CmGps)
                .IsFixedLength()
                .HasMaxLength(1);

            Property(t => t.CmSignalStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            Property(t => t.IpGps)
                .HasMaxLength(21);

            // Table & Column Mappings
            ToTable("universe");
            Property(t => t.VehicleId).HasColumnName("Vehicle_Id");
            Property(t => t.GpsProductId).HasColumnName("GPS_Product_Id");
            Property(t => t.CurrentDataDatetime).HasColumnName("CurrentData_Datetime");
            Property(t => t.CorrectDataId).HasColumnName("CorrectData_Id");
            Property(t => t.CorrectDataDatetime).HasColumnName("CorrectData_Datetime");
            Property(t => t.CmCommand).HasColumnName("CM_Command");
            Property(t => t.CmEngine).HasColumnName("CM_Engine");
            Property(t => t.CmMeter).HasColumnName("CM_Meter");
            Property(t => t.CmBatt).HasColumnName("CM_Batt");
            Property(t => t.FuelLevel).HasColumnName("FuelLevel");
            Property(t => t.CmTemp).HasColumnName("CM_Temp");
            Property(t => t.TempLevel).HasColumnName("TempLevel");
            Property(t => t.CmGps).HasColumnName("CM_GPS");
            Property(t => t.CmSignalStatus).HasColumnName("CM_SignalStatus");
            Property(t => t.Speed).HasColumnName("Speed");
            Property(t => t.Direction).HasColumnName("Direction");
            Property(t => t.IpGps).HasColumnName("IpGPS");
            Property(t => t.Port).HasColumnName("Port");
            Property(t => t.LaGoogle).HasColumnName("LaGoogle");
            Property(t => t.LongGoogle).HasColumnName("LongGoogle");
            Property(t => t.DisplayStatus).HasColumnName("Display_Status");
            Property(t => t.DriverId).HasColumnName("Driver_Id");
            Property(t => t.OrderId).HasColumnName("Order_Id");
            Property(t => t.FixOrderId).HasColumnName("FixOrder_Id");
            Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Relationships
            HasOptional(t => t.Driver)
                .WithMany(t => t.Universes)
                .HasForeignKey(d => d.DriverId);
            HasOptional(t => t.FixOrders)
                .WithMany(t => t.Universes)
                .HasForeignKey(d => d.FixOrderId);
            HasOptional(t => t.Order)
                .WithMany(t => t.Universes)
                .HasForeignKey(d => d.OrderId);
            HasOptional(t => t.ProductGps)
                .WithMany(t => t.Universes)
                .HasForeignKey(d => d.GpsProductId);
            HasOptional(t => t.UnCmBatt)
                .WithMany(t => t.Universes)
                .HasForeignKey(d => d.CmBatt);
            HasOptional(t => t.UnCmComm)
                .WithMany(t => t.Universes)
                .HasForeignKey(d => d.CmCommand);
            HasOptional(t => t.UnCmEngine)
                .WithMany(t => t.Universes)
                .HasForeignKey(d => d.CmEngine);
            HasOptional(t => t.UnCmGps)
                .WithMany(t => t.Universes)
                .HasForeignKey(d => d.CmGps);
            HasOptional(t => t.UnCmMeter)
                .WithMany(t => t.Universes)
                .HasForeignKey(d => d.CmMeter);
            HasRequired(t => t.UnCmSignal)
                .WithMany(t => t.Universes)
                .HasForeignKey(d => d.CmSignalStatus);
            HasOptional(t => t.UnCmTemp)
                .WithMany(t => t.Universes)
                .HasForeignKey(d => d.CmTemp);
            HasRequired(t => t.UnDisplayStatus)
                .WithMany(t => t.Universes)
                .HasForeignKey(d => d.DisplayStatus);
            HasRequired(t => t.Vehicle)
                .WithMany(t => t.Universes)
                .HasForeignKey(d => d.VehicleId);

        }
    }
}
