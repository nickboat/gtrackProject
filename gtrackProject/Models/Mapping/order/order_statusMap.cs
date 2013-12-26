using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace gtrackProject.Models.Mapping
{
    public class OrderStatusMap : EntityTypeConfiguration<OrderStatus>
    {
        public OrderStatusMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.StatusTh)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.StatusEn)
                .IsRequired()
                .HasMaxLength(15);

            // Table & Column Mappings
            this.ToTable("order_status");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.StatusTh).HasColumnName("Status_TH");
            this.Property(t => t.StatusEn).HasColumnName("Status_EN");
        }
    }
}
