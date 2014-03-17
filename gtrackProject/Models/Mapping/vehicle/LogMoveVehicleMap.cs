using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using gtrackProject.Models.vehicle;

namespace gtrackProject.Models.Mapping.vehicle
{
    public class LogMoveVehicleMap : EntityTypeConfiguration<LogMoveVehicle>
    {
        public LogMoveVehicleMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Table & Column Mappings
            ToTable("log_move_vehicle");
            Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Comment).HasColumnName("Comment");
            Property(t => t.HdAtFirst).HasColumnName("Hd_AtFirst");
            Property(t => t.IdCarAtFirst).HasColumnName("IdCar_AtFirst");
            Property(t => t.HdMoveTo).HasColumnName("Hd_MoveTo");
            Property(t => t.IdCarMoveTo).HasColumnName("IdCar_MoveTo");
            Property(t => t.VehicleId).HasColumnName("Vehicle_Id");
            Property(t => t.CreateBy).HasColumnName("CreateBy");
            Property(t => t.CreateDate).HasColumnName("CreateDate");

            // Relationships
            HasRequired(t => t.Vehicle)
                .WithMany(t => t.LogMoveVehicles)
                .HasForeignKey(d => d.VehicleId);
            HasRequired(t => t.CreateByEmployee)
                .WithMany(t => t.LogMoveVehicles)
                .HasForeignKey(d => d.CreateBy);
            HasRequired(t => t.FirstHd)
                .WithMany(t => t.LogMoveVehicles)
                .HasForeignKey(d => d.HdAtFirst);
            HasRequired(t => t.MoveHd)
                .WithMany(t => t.LogMoveVehicles)
                .HasForeignKey(d => d.HdMoveTo);
        }
    }
}