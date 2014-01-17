using System.Data.Entity.ModelConfiguration;
using gtrackProject.Models.driver;

namespace gtrackProject.Models.Mapping.driver
{
    public class DriverMap : EntityTypeConfiguration<Driver>
    {
        public DriverMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.TitleName)
                .HasMaxLength(6);

            Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(30);

            Property(t => t.LastNmae)
                .IsRequired()
                .HasMaxLength(30);

            Property(t => t.Gender)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            ToTable("drivers");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.IdCard).HasColumnName("IDCard");
            Property(t => t.ExpireCard).HasColumnName("ExpireCard");
            Property(t => t.TitleName).HasColumnName("TitleName");
            Property(t => t.FirstName).HasColumnName("FirstName");
            Property(t => t.LastNmae).HasColumnName("LastNmae");
            Property(t => t.BirthDate).HasColumnName("BirthDate");
            Property(t => t.Gender).HasColumnName("Gender");
            Property(t => t.DriverIdCard).HasColumnName("DriverIDCard");
            Property(t => t.ZipCode).HasColumnName("ZIPCode");
            Property(t => t.CategoryId).HasColumnName("Category_Id");
            Property(t => t.UserId).HasColumnName("User_Id");

            // Relationships
            HasRequired(t => t.DriverCategory)
                .WithMany(t => t.Drivers)
                .HasForeignKey(d => d.CategoryId);

        }
    }
}
