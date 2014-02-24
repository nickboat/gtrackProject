using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using gtrackProject.Models.product;

namespace gtrackProject.Models.Mapping.product
{
    public class LogMoveMap : EntityTypeConfiguration<LogMove>
    {
        public LogMoveMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Table & Column Mappings
            ToTable("log_move");
            Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Comment).HasColumnName("Comment");
            Property(t => t.Status).HasColumnName("Status");
            Property(t => t.VehicleMoveTo).HasColumnName("Vehicle_MoveTo");
            Property(t => t.VehicleAtFirst).HasColumnName("Vehicle_AtFirst");
            Property(t => t.GpsId).HasColumnName("Product_Id");
            Property(t => t.CreateBy).HasColumnName("CreateBy");
            Property(t => t.CreateDate).HasColumnName("CreateDate");

            // Relationships
            HasRequired(t => t.Gps)
                .WithMany(t => t.LogMoves)
                .HasForeignKey(d => d.GpsId);
            HasRequired(t => t.CreateByEmployee)
                .WithMany(t => t.LogMoves)
                .HasForeignKey(d => d.CreateBy);
            HasRequired(t => t.LogStatus)
                .WithMany(t => t.LogMoves)
                .HasForeignKey(d => d.Status);
            HasRequired(t => t.FirstVehicle)
                .WithMany(t => t.FirstLogMoves)
                .HasForeignKey(d => d.VehicleAtFirst);
            HasRequired(t => t.MoveVehicle)
                .WithMany(t => t.MoveLogMoves)
                .HasForeignKey(d => d.VehicleMoveTo);
        }
    }
}