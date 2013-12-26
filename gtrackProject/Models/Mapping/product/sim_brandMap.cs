using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace gtrackProject.Models.Mapping
{
    public class SimBrandMap : EntityTypeConfiguration<SimBrand>
    {
        public SimBrandMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.BrandName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("sim_brand");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.BrandName).HasColumnName("BrandName");
        }
    }
}
