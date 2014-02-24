using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using gtrackProject.Models.product;

namespace gtrackProject.Models.Mapping.product
{
    public class SimFeeTypeMap : EntityTypeConfiguration<SimFeeType>
    {
        public SimFeeTypeMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.PaymentName)
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("product_sim_feetype");
            Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.PaymentName).HasColumnName("PaymentName");
        }
    }
}