using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using gtrackProject.Models.order;

namespace gtrackProject.Models.Mapping.order
{
    public class OrderExtendTypeMap : EntityTypeConfiguration<OrderExtendType>
    {
        public OrderExtendTypeMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.TypeName)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            ToTable("order_extend_type");
            Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.TypeName).HasColumnName("TypeName");
            Property(t => t.Value).HasColumnName("Value");
        }
    }
}
