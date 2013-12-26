using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace gtrackProject.Models.Mapping
{
    public class DriverMap : EntityTypeConfiguration<Driver>
    {
        public DriverMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.TitleName)
                .HasMaxLength(6);

            this.Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.LastNmae)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.Gender)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("drivers");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdCard).HasColumnName("IDCard");
            this.Property(t => t.ExpireCard).HasColumnName("ExpireCard");
            this.Property(t => t.TitleName).HasColumnName("TitleName");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastNmae).HasColumnName("LastNmae");
            this.Property(t => t.BirthDate).HasColumnName("BirthDate");
            this.Property(t => t.Gender).HasColumnName("Gender");
            this.Property(t => t.DriverIdCard).HasColumnName("DriverIDCard");
            this.Property(t => t.ZipCode).HasColumnName("ZIPCode");
            this.Property(t => t.CategoryId).HasColumnName("Category_Id");
            this.Property(t => t.UserId).HasColumnName("User_Id");

            // Relationships
            this.HasRequired(t => t.DriverCategory)
                .WithMany(t => t.Drivers)
                .HasForeignKey(d => d.CategoryId);

        }
    }
}
