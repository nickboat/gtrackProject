using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using gtrackProject.Models.product;

namespace gtrackProject.Models.Mapping.product
{
    public class LogSwapMap : EntityTypeConfiguration<LogSwap>
    {
        public LogSwapMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Table & Column Mappings
            ToTable("log_swap");
            Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Comment).HasColumnName("Comment");
            Property(t => t.Status).HasColumnName("Status");
            Property(t => t.VehicleB).HasColumnName("Vehicle_B");
            Property(t => t.VehicleA).HasColumnName("Vehicle_A");
            Property(t => t.GpsB).HasColumnName("Product_B");
            Property(t => t.GpsA).HasColumnName("Product_A");
            Property(t => t.CreateBy).HasColumnName("CreateBy");
            Property(t => t.CreateDate).HasColumnName("CreateDate");

            // Relationships
            HasRequired(t => t.LogStatus)
                .WithMany(t => t.LogSwaps)
                .HasForeignKey(d => d.Status);
            HasRequired(t => t.CreateByEmployee)
                .WithMany(t => t.LogSwaps)
                .HasForeignKey(d => d.CreateBy);
            HasRequired(t => t.Gps1)
                .WithMany(t => t.LogASwaps)
                .HasForeignKey(d => d.GpsA);
            HasRequired(t => t.Gps2)
                .WithMany(t => t.LogBSwaps)
                .HasForeignKey(d => d.GpsB);
            HasRequired(t => t.Vehicle1)
                .WithMany(t => t.LogASwaps)
                .HasForeignKey(d => d.VehicleA);
            HasRequired(t => t.Vehicle2)
                .WithMany(t => t.LogBSwaps)
                .HasForeignKey(d => d.VehicleB);
        }
    }
}