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
            HasKey(t => t.Id);

            // Properties
            Property(t => t.FullName)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.Phone)
                .IsRequired()
                .HasMaxLength(10);

            Property(t => t.AspId)
                .IsRequired()
                .HasMaxLength(128);

            Property(t => t.Gender)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            ToTable("employee");
            Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.FullName).HasColumnName("FullName");
            Property(t => t.Phone).HasColumnName("Phone");
            Property(t => t.AspId).HasColumnName("AspId");
            Property(t => t.Gender).HasColumnName("Gender");
            Property(t => t.BirthDate).HasColumnName("BirthDate");
        }
    }
}
