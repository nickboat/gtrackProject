using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using gtrackProject.Models.account;

namespace gtrackProject.Models.Mapping
{
    public class EmployeeMap : EntityTypeConfiguration<Employee>
    {
        public EmployeeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.FullName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Phone)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.AspId)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.Gender)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("employee");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.FullName).HasColumnName("FullName");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.AspId).HasColumnName("AspId");
            this.Property(t => t.Gender).HasColumnName("Gender");
            this.Property(t => t.BirthDate).HasColumnName("BirthDate");
        }
    }
}
