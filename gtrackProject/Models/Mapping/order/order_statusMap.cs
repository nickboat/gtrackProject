using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using gtrackProject.Models.order;

namespace gtrackProject.Models.Mapping.order
{
    public class OrderStatusMap : EntityTypeConfiguration<OrderStatus>
    {
        public OrderStatusMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.StatusTh)
                .IsRequired()
                .HasMaxLength(15);

            Property(t => t.StatusEn)
                .IsRequired()
                .HasMaxLength(15);

            // Table & Column Mappings
            ToTable("order_status");
            Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.StatusTh).HasColumnName("Status_TH");
            Property(t => t.StatusEn).HasColumnName("Status_EN");
        }
    }
}
