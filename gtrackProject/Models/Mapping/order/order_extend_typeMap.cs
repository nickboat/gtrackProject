using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace gtrackProject.Models.Mapping
{
    public class OrderExtendTypeMap : EntityTypeConfiguration<OrderExtendType>
    {
        public OrderExtendTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.TypeName)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("order_extend_type");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.TypeName).HasColumnName("TypeName");
            this.Property(t => t.Value).HasColumnName("Value");
        }
    }
}
