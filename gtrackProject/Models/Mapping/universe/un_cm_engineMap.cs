using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace gtrackProject.Models.Mapping
{
    public class UnCmEngineMap : EntityTypeConfiguration<UnCmEngine>
    {
        public UnCmEngineMap()
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

            this.Property(t => t.MsgTh)
                .HasMaxLength(255);

            this.Property(t => t.MsgEn)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("un_cm_engine");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.MsgTh).HasColumnName("Msg_TH");
            this.Property(t => t.MsgEn).HasColumnName("Msg_EN");
        }
    }
}
