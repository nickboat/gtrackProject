using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace gtrackProject.Models.Mapping
{
    public class order_statusMap : EntityTypeConfiguration<order_status>
    {
        public order_statusMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Status_TH)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.Status_EN)
                .IsRequired()
                .HasMaxLength(15);

            // Table & Column Mappings
            this.ToTable("order_status");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Status_TH).HasColumnName("Status_TH");
            this.Property(t => t.Status_EN).HasColumnName("Status_EN");
        }
    }
}
