using System.Data.Entity.ModelConfiguration;
using gtrackProject.Models.product;

namespace gtrackProject.Models.Mapping.product
{
    public class SimBrandMap : EntityTypeConfiguration<SimBrand>
    {
        public SimBrandMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.BrandName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("sim_brand");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.BrandName).HasColumnName("BrandName");
        }
    }
}
