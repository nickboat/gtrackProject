using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace gtrackProject.Models.Mapping
{
    public class un_cm_battMap : EntityTypeConfiguration<un_cm_batt>
    {
        public un_cm_battMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Msg_TH)
                .HasMaxLength(255);

            this.Property(t => t.Msg_EN)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("un_cm_batt");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Msg_TH).HasColumnName("Msg_TH");
            this.Property(t => t.Msg_EN).HasColumnName("Msg_EN");
        }
    }
}
