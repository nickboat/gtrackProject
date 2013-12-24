using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace gtrackProject.Models.Mapping
{
    public class lp_typeMap : EntityTypeConfiguration<lp_type>
    {
        public lp_typeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(20);

            this.Property(t => t.Meaning)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("lp_type");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Meaning).HasColumnName("Meaning");
        }
    }
}
