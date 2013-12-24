using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace gtrackProject.Models.Mapping
{
    public class fix_ordersMap : EntityTypeConfiguration<fix_orders>
    {
        public fix_ordersMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Comment)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("fix_orders");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreateBy).HasColumnName("CreateBy");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.CurrentUser).HasColumnName("CurrentUser");
            this.Property(t => t.HeadInstall).HasColumnName("HeadInstall");
            this.Property(t => t.Comment).HasColumnName("Comment");
            this.Property(t => t.Status).HasColumnName("Status");
        }
    }
}
