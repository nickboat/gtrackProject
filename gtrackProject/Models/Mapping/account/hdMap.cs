using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace gtrackProject.Models.Mapping
{
    public class HdMap : EntityTypeConfiguration<Hd>
    {
        public HdMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Value)
                .IsRequired()
                .HasMaxLength(3);

            this.Property(t => t.Code)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(6);

            this.Property(t => t.Name)
                .HasMaxLength(50);

            this.Property(t => t.TableName)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("hds");
            this.Property(t => t.Value).HasColumnName("Value");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.TableName).HasColumnName("TableName");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.HdIdUpline).HasColumnName("HD_Id_upline");

            // Relationships
            this.HasOptional(t => t.ThisHd)
                .WithMany(t => t.HdDownLines)
                .HasForeignKey(d => d.HdIdUpline);

        }
    }
}
