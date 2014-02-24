using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using gtrackProject.Models.product;

namespace gtrackProject.Models.Mapping.product
{
    public class LogSimMap : EntityTypeConfiguration<LogSim>
    {
        public LogSimMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Table & Column Mappings
            ToTable("log_sim");
            Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.SimAtFirst).HasColumnName("Sim_AtFirst");
            Property(t => t.SimNew).HasColumnName("Sim_New");
            Property(t => t.GpsId).HasColumnName("Product_Id");
            Property(t => t.CreateBy).HasColumnName("CreateBy");
            Property(t => t.CreateDate).HasColumnName("CreateDate");

            // Relationships
            HasRequired(t => t.Gps)
                .WithMany(t => t.LogSims)
                .HasForeignKey(d => d.GpsId);
            HasRequired(t => t.CreateByEmployee)
                .WithMany(t => t.LogSims)
                .HasForeignKey(d => d.CreateBy);
            HasRequired(t => t.OldSim)
                .WithMany(t => t.LogSimOlds)
                .HasForeignKey(d => d.SimAtFirst);
            HasRequired(t => t.NewSim)
                .WithMany(t => t.LogSimNews)
                .HasForeignKey(d => d.SimNew);
        }
    }
}