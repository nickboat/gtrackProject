using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using gtrackProject.Models.order;

namespace gtrackProject.Models.Mapping.order
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderProcessStateMap : EntityTypeConfiguration<OrderProcessState>
    {
        public OrderProcessStateMap()
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
            ToTable("order_process_state");
            Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.StatusTh).HasColumnName("Status_TH");
            Property(t => t.StatusEn).HasColumnName("Status_EN");
        }
    }
}
