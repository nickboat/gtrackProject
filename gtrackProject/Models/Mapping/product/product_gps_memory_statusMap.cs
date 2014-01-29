using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using gtrackProject.Models.product;

namespace gtrackProject.Models.Mapping.product
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductGpsMemoryStatusMap : EntityTypeConfiguration<ProductGpsMemoryStatus>
    {
        public ProductGpsMemoryStatusMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.Val)
                .IsRequired()
                .HasMaxLength(1);

            // Table & Column Mappings
            ToTable("product_gps_memory_status");
            Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.Val).HasColumnName("Val");
        }
    }
}