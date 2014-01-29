using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using gtrackProject.Models.order;

namespace gtrackProject.Models.Mapping.order
{
    public class OrderMap : EntityTypeConfiguration<Order>
    {
        public OrderMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Comment)
                .HasMaxLength(100);

            // Table & Column Mappings
            ToTable("orders");
            Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.CreateBy).HasColumnName("CreateBy");
            Property(t => t.CreateDate).HasColumnName("CreateDate");
            Property(t => t.CurrentUser).HasColumnName("CurrentUser");
            Property(t => t.HeadInstall).HasColumnName("HeadInstall");
            Property(t => t.HdId).HasColumnName("Hd_id");
            Property(t => t.Version).HasColumnName("Version");
            Property(t => t.Quantity).HasColumnName("Quantity");
            Property(t => t.PricePerUnit).HasColumnName("PricePerUnit");
            Property(t => t.FeePerYear).HasColumnName("FeePerYear");
            Property(t => t.Comment).HasColumnName("Comment");
            Property(t => t.Status).HasColumnName("Status");
            Property(t => t.Deadline).HasColumnName("Deadline");
            Property(t => t.ExtendTypeId).HasColumnName("ExtendType_Id");

            // Relationships
            HasRequired(t => t.CreateByEmployee)
                .WithMany(t => t.OrderCreates)
                .HasForeignKey(d => d.CreateBy);
            HasRequired(t => t.CurrentUserEmployee)
                .WithMany(t => t.OrderCurrents)
                .HasForeignKey(d => d.HeadInstall);
            HasRequired(t => t.Hd)
                .WithMany(t => t.Orders)
                .HasForeignKey(d => d.HdId);
            HasOptional(t => t.OrderExtendType)
                .WithMany(t => t.Orders)
                .HasForeignKey(d => d.ExtendTypeId);
            HasOptional(t => t.OrderStatus)
                .WithMany(t => t.Orders)
                .HasForeignKey(d => d.Status);
            HasOptional(t => t.ProductGpsVersion)
                .WithMany(t => t.Orders)
                .HasForeignKey(d => d.Version);

        }
    }
}
