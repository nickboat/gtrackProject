using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using gtrackProject.Models.account;

namespace gtrackProject.Models.Mapping.account
{
    public class CustomerMap : EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Asp_Id)
                .IsRequired()
                .HasMaxLength(128);

            Property(t => t.FullName)
                .IsRequired()
                .HasMaxLength(100);

            Property(t => t.Phone)
                .IsRequired()
                .HasMaxLength(10);

            Property(t => t.Email)
                .HasMaxLength(100);

            Property(t => t.CompanyName)
                .HasMaxLength(100);

            // Table & Column Mappings
            ToTable("customer");
            Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Asp_Id).HasColumnName("Asp_Id");
            Property(t => t.Hd_Id).HasColumnName("Hd_Id");
            Property(t => t.FullName).HasColumnName("FullName");
            Property(t => t.Phone).HasColumnName("Phone");
            Property(t => t.Email).HasColumnName("Email");
            Property(t => t.CompanyName).HasColumnName("CompanyName");
            Property(t => t.CreateDate).HasColumnName("CreateDate");
            Property(t => t.CreateBy).HasColumnName("CreateBy");
            Property(t => t.Leader).HasColumnName("Leader");
            Property(t => t.IdCard).HasColumnName("IDCard");

            // Relationships
            HasRequired(t => t.Hd)
                .WithMany(t => t.Customers)
                .HasForeignKey(d => d.Hd_Id);

            HasOptional(t => t.LeaderCustomer)
                .WithMany(t => t.ThisLeaders)
                .HasForeignKey(d => d.Leader);

            HasOptional(t => t.CreateByEmployee)
                .WithMany(t => t.CustCreates)
                .HasForeignKey(d => d.CreateBy);
        }
    }
}
