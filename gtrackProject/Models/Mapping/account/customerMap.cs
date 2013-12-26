using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace gtrackProject.Models.Mapping
{
    public class CustomerMap : EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Asp_Id)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.FullName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Phone)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.Email)
                .HasMaxLength(100);

            this.Property(t => t.CompanyName)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("customer");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Asp_Id).HasColumnName("Asp_Id");
            this.Property(t => t.Hd_Id).HasColumnName("Hd_Id");
            this.Property(t => t.FullName).HasColumnName("FullName");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.CompanyName).HasColumnName("CompanyName");
        }
    }
}
