using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using gtrackProject.Models.product;

namespace gtrackProject.Models.Mapping.product
{
    public class SimMap : EntityTypeConfiguration<Sim>
    {
        public SimMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Number)
                .HasMaxLength(10);

            // Table & Column Mappings
            ToTable("product_sim_feetype");
            Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Number).HasColumnName("Number");
            Property(t => t.Network).HasColumnName("Network");
            Property(t => t.FeeType).HasColumnName("FeeType");
            Property(t => t.Status).HasColumnName("Status");

            // Relationships
            HasRequired(t => t.SimNetwork)
                .WithMany(t => t.Sims)
                .HasForeignKey(d => d.Network);
            HasRequired(t => t.SimFeeType)
                .WithMany(t => t.Sims)
                .HasForeignKey(d => d.FeeType);
            HasRequired(t => t.SimStatus)
                .WithMany(t => t.Sims)
                .HasForeignKey(d => d.Status);
        }
    }
}