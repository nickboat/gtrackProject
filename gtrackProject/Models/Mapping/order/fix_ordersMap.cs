using System.Data.Entity.ModelConfiguration;
using gtrackProject.Models.order;

namespace gtrackProject.Models.Mapping.order
{
    public class FixOrdersMap : EntityTypeConfiguration<FixOrders>
    {
        public FixOrdersMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Comment)
                .HasMaxLength(100);

            // Table & Column Mappings
            ToTable("fix_orders");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.CreateBy).HasColumnName("CreateBy");
            Property(t => t.CreateDate).HasColumnName("CreateDate");
            Property(t => t.CurrentUser).HasColumnName("CurrentUser");
            Property(t => t.HeadInstall).HasColumnName("HeadInstall");
            Property(t => t.Comment).HasColumnName("Comment");
            Property(t => t.Status).HasColumnName("Status");

            // Relationships
            HasOptional(t => t.CreateByEmployee)
                .WithMany(t => t.FixCreates)
                .HasForeignKey(d => d.CreateBy);
            HasRequired(t => t.CurrentUsermployee)
                .WithMany(t => t.FixCurrents)
                .HasForeignKey(d => d.HeadInstall);
            HasOptional(t => t.OrderStatus)
                .WithMany(t => t.FixOrders)
                .HasForeignKey(d => d.Status);
                        
        }
    }
}
