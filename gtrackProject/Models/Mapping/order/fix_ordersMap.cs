using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using gtrackProject.Models.order;

namespace gtrackProject.Models.Mapping.order
{
    public class FixOrdersMap : EntityTypeConfiguration<FixOrders>
    {
        public FixOrdersMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Comment)
                .HasMaxLength(100);

            // Table & Column Mappings
            ToTable("fix_orders");
            Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.CreateBy).HasColumnName("CreateBy");
            Property(t => t.CreateDate).HasColumnName("CreateDate");
            Property(t => t.CurrentUser).HasColumnName("CurrentUser");
            Property(t => t.HeadInstall).HasColumnName("HeadInstall");
            Property(t => t.Comment).HasColumnName("Comment");
            Property(t => t.State).HasColumnName("Status");

            // Relationships
            HasOptional(t => t.CreateByEmployee)
                .WithMany(t => t.FixCreates)
                .HasForeignKey(d => d.CreateBy);
            HasRequired(t => t.CurrentUserEmployee)
                .WithMany(t => t.FixCurrents)
                .HasForeignKey(d => d.CurrentUser);
            HasRequired(t => t.InstallByEmployee)
                .WithMany(t => t.FixInstalls)
                .HasForeignKey(d => d.HeadInstall);
            HasOptional(t => t.OrderProcessState)
                .WithMany(t => t.FixOrders)
                .HasForeignKey(d => d.State);
                        
        }
    }
}
