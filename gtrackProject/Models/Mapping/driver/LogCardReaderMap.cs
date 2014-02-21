using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using gtrackProject.Models.driver;

namespace gtrackProject.Models.Mapping.driver
{
    public class LogCardReaderMap : EntityTypeConfiguration<LogCardReader>
    {
        public LogCardReaderMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Table & Column Mappings
            ToTable("log_cardreader");
            Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.AtDateTime).HasColumnName("AtDateTime");
            Property(t => t.DriverId).HasColumnName("Driver_Id");
            Property(t => t.VehicleId).HasColumnName("Vehicle_Id");
            Property(t => t.ProductGpsId).HasColumnName("Product_Id");

            // Relationships
            HasRequired(t => t.Driver)
                .WithMany(t => t.LogCardReaders)
                .HasForeignKey(d => d.DriverId);

            HasRequired(t => t.Vehicle)
                .WithMany(t => t.LogCardReaders)
                .HasForeignKey(d => d.VehicleId);

            HasRequired(t => t.ProductGps)
                .WithMany(t => t.LogCardReaders)
                .HasForeignKey(d => d.ProductGpsId);
        }
    }
}