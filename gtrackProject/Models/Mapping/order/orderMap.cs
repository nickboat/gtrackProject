using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using gtrackProject.Models.order;

namespace gtrackProject.Models.Mapping
{
    public class OrderMap : EntityTypeConfiguration<Order>
    {
        public OrderMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Comment)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("orders");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreateBy).HasColumnName("CreateBy");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.CurrentUser).HasColumnName("CurrentUser");
            this.Property(t => t.HeadInstall).HasColumnName("HeadInstall");
            this.Property(t => t.HdId).HasColumnName("Hd_id");
            this.Property(t => t.Version).HasColumnName("Version");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.PricePerUnit).HasColumnName("PricePerUnit");
            this.Property(t => t.FeePerYear).HasColumnName("FeePerYear");
            this.Property(t => t.Comment).HasColumnName("Comment");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Deadline).HasColumnName("Deadline");
            this.Property(t => t.ExtendTypeId).HasColumnName("ExtendType_Id");

            // Relationships
            this.HasRequired(t => t.CreateByEmployee)
                .WithMany(t => t.OrderCreates)
                .HasForeignKey(d => d.CreateBy);
            this.HasRequired(t => t.CurrentUserEmployee)
                .WithMany(t => t.OrderCurrents)
                .HasForeignKey(d => d.HeadInstall);
            this.HasRequired(t => t.Hd)
                .WithMany(t => t.Orders)
                .HasForeignKey(d => d.HdId);
            this.HasOptional(t => t.OrderExtendType)
                .WithMany(t => t.Orders)
                .HasForeignKey(d => d.ExtendTypeId);
            this.HasOptional(t => t.OrderStatus)
                .WithMany(t => t.Orders)
                .HasForeignKey(d => d.Status);
            this.HasOptional(t => t.ProductGpsVersion)
                .WithMany(t => t.Orders)
                .HasForeignKey(d => d.Version);

        }
    }
}
