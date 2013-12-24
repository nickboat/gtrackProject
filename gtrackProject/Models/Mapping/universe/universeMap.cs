using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace gtrackProject.Models.Mapping
{
    public class universeMap : EntityTypeConfiguration<universe>
    {
        public universeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.CM_Command)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.CM_Engine)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.CM_Meter)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.CM_Batt)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.CM_Temp)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.CM_GPS)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.CM_SignalStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.IpGPS)
                .HasMaxLength(21);

            // Table & Column Mappings
            this.ToTable("universe");
            this.Property(t => t.Vehicle_Id).HasColumnName("Vehicle_Id");
            this.Property(t => t.GPS_Product_Id).HasColumnName("GPS_Product_Id");
            this.Property(t => t.CurrentData_Datetime).HasColumnName("CurrentData_Datetime");
            this.Property(t => t.CorrectData_Id).HasColumnName("CorrectData_Id");
            this.Property(t => t.CorrectData_Datetime).HasColumnName("CorrectData_Datetime");
            this.Property(t => t.CM_Command).HasColumnName("CM_Command");
            this.Property(t => t.CM_Engine).HasColumnName("CM_Engine");
            this.Property(t => t.CM_Meter).HasColumnName("CM_Meter");
            this.Property(t => t.CM_Batt).HasColumnName("CM_Batt");
            this.Property(t => t.FuelLevel).HasColumnName("FuelLevel");
            this.Property(t => t.CM_Temp).HasColumnName("CM_Temp");
            this.Property(t => t.TempLevel).HasColumnName("TempLevel");
            this.Property(t => t.CM_GPS).HasColumnName("CM_GPS");
            this.Property(t => t.CM_SignalStatus).HasColumnName("CM_SignalStatus");
            this.Property(t => t.Speed).HasColumnName("Speed");
            this.Property(t => t.Direction).HasColumnName("Direction");
            this.Property(t => t.IpGPS).HasColumnName("IpGPS");
            this.Property(t => t.Port).HasColumnName("Port");
            this.Property(t => t.LaGoogle).HasColumnName("LaGoogle");
            this.Property(t => t.LongGoogle).HasColumnName("LongGoogle");
            this.Property(t => t.Display_Status).HasColumnName("Display_Status");
            this.Property(t => t.Driver_Id).HasColumnName("Driver_Id");
            this.Property(t => t.Order_Id).HasColumnName("Order_Id");
            this.Property(t => t.FixOrder_Id).HasColumnName("FixOrder_Id");
            this.Property(t => t.Id).HasColumnName("Id");

            // Relationships
            this.HasOptional(t => t.driver)
                .WithMany(t => t.universes)
                .HasForeignKey(d => d.Driver_Id);
            this.HasOptional(t => t.fix_orders)
                .WithMany(t => t.universes)
                .HasForeignKey(d => d.FixOrder_Id);
            this.HasOptional(t => t.order)
                .WithMany(t => t.universes)
                .HasForeignKey(d => d.Order_Id);
            this.HasOptional(t => t.product_gps)
                .WithMany(t => t.universes)
                .HasForeignKey(d => d.GPS_Product_Id);
            this.HasOptional(t => t.un_cm_batt)
                .WithMany(t => t.universes)
                .HasForeignKey(d => d.CM_Batt);
            this.HasOptional(t => t.un_cm_comm)
                .WithMany(t => t.universes)
                .HasForeignKey(d => d.CM_Command);
            this.HasOptional(t => t.un_cm_engine)
                .WithMany(t => t.universes)
                .HasForeignKey(d => d.CM_Engine);
            this.HasOptional(t => t.un_cm_gps)
                .WithMany(t => t.universes)
                .HasForeignKey(d => d.CM_GPS);
            this.HasOptional(t => t.un_cm_meter)
                .WithMany(t => t.universes)
                .HasForeignKey(d => d.CM_Meter);
            this.HasRequired(t => t.un_cm_signal)
                .WithMany(t => t.universes)
                .HasForeignKey(d => d.CM_SignalStatus);
            this.HasOptional(t => t.un_cm_temp)
                .WithMany(t => t.universes)
                .HasForeignKey(d => d.CM_Temp);
            this.HasRequired(t => t.un_display_status)
                .WithMany(t => t.universes)
                .HasForeignKey(d => d.Display_Status);
            this.HasRequired(t => t.vehicle)
                .WithMany(t => t.universes)
                .HasForeignKey(d => d.Vehicle_Id);

        }
    }
}
