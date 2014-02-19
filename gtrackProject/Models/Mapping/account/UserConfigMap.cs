using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using gtrackProject.Models.account;

namespace gtrackProject.Models.Mapping.account
{
    public class UserConfigMap : EntityTypeConfiguration<UserConfig>
    {
        public UserConfigMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Asp_Id)
                .IsRequired()
                .HasMaxLength(128);

            // Table & Column Mappings
            ToTable("user_config");
            Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Asp_Id).HasColumnName("Asp_Id");
            Property(t => t.UseHelper).HasColumnName("UseHelper");
            Property(t => t.UseThai).HasColumnName("UseThai");
        }
    }
}