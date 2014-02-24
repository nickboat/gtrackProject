using System.Data.Entity.ModelConfiguration;
using gtrackProject.Models.universe;

namespace gtrackProject.Models.Mapping.universe
{
    public class UnCmSignalMap : EntityTypeConfiguration<UnCmSignal>
    {
        public UnCmSignalMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Id)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.MsgTh)
                .HasMaxLength(255);

            Property(t => t.MsgEn)
                .HasMaxLength(255);

            // Table & Column Mappings
            ToTable("un_cm_signal");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.MsgTh).HasColumnName("Msg_TH");
            Property(t => t.MsgEn).HasColumnName("Msg_EN");
        }
    }
}
