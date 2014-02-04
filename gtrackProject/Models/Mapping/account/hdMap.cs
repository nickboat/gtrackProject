using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using gtrackProject.Models.account;

namespace gtrackProject.Models.Mapping
{
    public class HdMap : EntityTypeConfiguration<Hd>
    {
        public HdMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Value)
                .IsRequired()
                .HasMaxLength(3);

            Property(t => t.Code)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(6);

            Property(t => t.Name)
                .HasMaxLength(50);

            Property(t => t.TableName)
                .HasMaxLength(20);

            // Table & Column Mappings
            ToTable("hds");
            Property(t => t.Value).HasColumnName("Value");
            Property(t => t.Code).HasColumnName("Code");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.TableName).HasColumnName("TableName");
            Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.HdIdUpline).HasColumnName("HD_Id_upline");

            // Relationships
            HasOptional(t => t.Upline)
                .WithMany(t => t.HdDownLines)
                .HasForeignKey(d => d.HdIdUpline);

        }
    }
}
