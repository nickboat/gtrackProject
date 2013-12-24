using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace gtrackProject.Models.Mapping
{
    public class orderMap : EntityTypeConfiguration<order>
    {
        public orderMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.HD)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.Comment)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("orders");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreateBy).HasColumnName("CreateBy");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.CurrentUser).HasColumnName("CurrentUser");
            this.Property(t => t.HeadInstall).HasColumnName("HeadInstall");
            this.Property(t => t.HD).HasColumnName("HD");
            this.Property(t => t.Version).HasColumnName("Version");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.PricePerUnit).HasColumnName("PricePerUnit");
            this.Property(t => t.FeePerYear).HasColumnName("FeePerYear");
            this.Property(t => t.Comment).HasColumnName("Comment");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Deadline).HasColumnName("Deadline");
            this.Property(t => t.ExtendType_Id).HasColumnName("ExtendType_Id");

            // Relationships
            this.HasOptional(t => t.order_extend_type)
                .WithMany(t => t.orders)
                .HasForeignKey(d => d.ExtendType_Id);
            this.HasOptional(t => t.order_status)
                .WithMany(t => t.orders)
                .HasForeignKey(d => d.Status);
            this.HasOptional(t => t.product_gps_version)
                .WithMany(t => t.orders)
                .HasForeignKey(d => d.Version);

        }
    }
}
