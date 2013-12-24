using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace gtrackProject.Models.Mapping
{
    public class driverMap : EntityTypeConfiguration<driver>
    {
        public driverMap()
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
            this.Property(t => t.IDCard).HasColumnName("IDCard");
            this.Property(t => t.ExpireCard).HasColumnName("ExpireCard");
            this.Property(t => t.TitleName).HasColumnName("TitleName");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastNmae).HasColumnName("LastNmae");
            this.Property(t => t.BirthDate).HasColumnName("BirthDate");
            this.Property(t => t.Gender).HasColumnName("Gender");
            this.Property(t => t.DriverIDCard).HasColumnName("DriverIDCard");
            this.Property(t => t.ZIPCode).HasColumnName("ZIPCode");
            this.Property(t => t.Category_Id).HasColumnName("Category_Id");
            this.Property(t => t.User_Id).HasColumnName("User_Id");

            // Relationships
            this.HasRequired(t => t.driver_category)
                .WithMany(t => t.drivers)
                .HasForeignKey(d => d.Category_Id);

        }
    }
}
