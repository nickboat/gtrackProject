using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace gtrackProject.Models.Mapping
{
    public class sim_payment_typeMap : EntityTypeConfiguration<sim_payment_type>
    {
        public sim_payment_typeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.PaymentName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("sim_payment_type");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.PaymentName).HasColumnName("PaymentName");
        }
    }
}
