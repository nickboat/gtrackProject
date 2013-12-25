using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace gtrackProject.Models.Mapping
{
    public class user_profileMap : EntityTypeConfiguration<user_profile>
    {
        public user_profileMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ASP_Id)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.Email)
                .HasMaxLength(50);

            this.Property(t => t.Phone)
                .HasMaxLength(15);

            this.Property(t => t.Address)
                .HasMaxLength(255);

            this.Property(t => t.Fullname)
                .HasMaxLength(100);

            this.Property(t => t.Company_Name)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("user_profile");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ASP_Id).HasColumnName("ASP_Id");
            this.Property(t => t.HD_Id).HasColumnName("HD_Id");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.Fullname).HasColumnName("Fullname");
            this.Property(t => t.Company_Name).HasColumnName("Company_Name");

            // Relationships
            this.HasRequired(t => t.hd)
                .WithMany(t => t.user_profile)
                .HasForeignKey(d => d.HD_Id);

        }
    }
}
