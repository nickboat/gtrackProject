using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using gtrackProject.Models.product;

namespace gtrackProject.Models.Mapping.product
{
    public class LogFeeMap : EntityTypeConfiguration<LogFee>
    {
        public LogFeeMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Table & Column Mappings
            ToTable("log_fee");
            Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.FeePerYear).HasColumnName("FeePerYear");
            Property(t => t.Year).HasColumnName("Year");
            Property(t => t.StartDate).HasColumnName("StartDate");
            Property(t => t.ExpireDate).HasColumnName("ExpireDate");
            Property(t => t.GpsId).HasColumnName("ProductGPS_Id");
            Property(t => t.CreateBy).HasColumnName("CreateBy");
            Property(t => t.CreateDate).HasColumnName("CreateDate");
            
            // Relationships
            HasRequired(t => t.Gps)
                .WithMany(t => t.LogFees)
                .HasForeignKey(d => d.GpsId);
            HasRequired(t => t.CreateByEmployee)
                .WithMany(t => t.LogFees)
                .HasForeignKey(d => d.CreateBy);
        }
    }
}