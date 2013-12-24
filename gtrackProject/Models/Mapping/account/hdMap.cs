using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace gtrackProject.Models.Mapping
{
    public class hdMap : EntityTypeConfiguration<hd>
    {
        public hdMap()
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
            this.Property(t => t.HD_Id_upline).HasColumnName("HD_Id_upline");

            // Relationships
            this.HasOptional(t => t.up_hd)
                .WithMany(t => t.up_hds)
                .HasForeignKey(d => d.HD_Id_upline);

        }
    }
}
