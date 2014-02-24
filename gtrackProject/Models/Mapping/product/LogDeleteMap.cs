using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using gtrackProject.Models.product;

namespace gtrackProject.Models.Mapping.product
{
    public class LogDeleteMap : EntityTypeConfiguration<LogDelete>
    {
        public LogDeleteMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Table & Column Mappings
            ToTable("log_delete");
            Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Comment).HasColumnName("Comment");
            Property(t => t.Status).HasColumnName("Status");
            Property(t => t.VehicleId).HasColumnName("Vehicle_Id");
            Property(t => t.GpsId).HasColumnName("Product_Id");
            Property(t => t.CreateBy).HasColumnName("CreateBy");
            Property(t => t.CreateDate).HasColumnName("CreateDate");

            // Relationships
            HasRequired(t => t.Gps)
                .WithMany(t => t.LogDeletes)
                .HasForeignKey(d => d.GpsId);
            HasRequired(t => t.CreateByEmployee)
                .WithMany(t => t.LogDeletes)
                .HasForeignKey(d => d.CreateBy);
            HasRequired(t => t.LogStatus)
                .WithMany(t => t.LogDeletes)
                .HasForeignKey(d => d.Status);
            HasRequired(t => t.Vehicle)
                .WithMany(t => t.LogDeletes)
                .HasForeignKey(d => d.VehicleId);
        }
    }
}