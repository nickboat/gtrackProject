using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace gtrackProject.Models.Mapping
{
    public class product_gpsMap : EntityTypeConfiguration<product_gps>
    {
        public product_gpsMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.SIM_Num)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.Serial)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.ErrProductComment)
                .HasMaxLength(150);

            this.Property(t => t.BadComment)
                .HasMaxLength(150);

            this.Property(t => t.UnuseableComment)
                .HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("product_gps");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SIM_Num).HasColumnName("SIM_Num");
            this.Property(t => t.SIM_Brand_Id).HasColumnName("SIM_Brand_Id");
            this.Property(t => t.SIM_Payment_Type_Id).HasColumnName("SIM_Payment_Type_Id");
            this.Property(t => t.Serial).HasColumnName("Serial");
            this.Property(t => t.Version).HasColumnName("Version");
            this.Property(t => t.CreateBy).HasColumnName("CreateBy");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.StockBy).HasColumnName("StockBy");
            this.Property(t => t.StockDate).HasColumnName("StockDate");
            this.Property(t => t.QCBy).HasColumnName("QCBy");
            this.Property(t => t.QCDate).HasColumnName("QCDate");
            this.Property(t => t.InstallBy).HasColumnName("InstallBy");
            this.Property(t => t.InstallDate).HasColumnName("InstallDate");
            this.Property(t => t.ErrProductComment).HasColumnName("ErrProductComment");
            this.Property(t => t.BadBy).HasColumnName("BadBy");
            this.Property(t => t.BadDate).HasColumnName("BadDate");
            this.Property(t => t.BadComment).HasColumnName("BadComment");
            this.Property(t => t.UnuseableBy).HasColumnName("UnuseableBy");
            this.Property(t => t.UnuseableDate).HasColumnName("UnuseableDate");
            this.Property(t => t.UnuseableComment).HasColumnName("UnuseableComment");
            this.Property(t => t.ExpireDate).HasColumnName("ExpireDate");
            this.Property(t => t.LastExtendDate).HasColumnName("LastExtendDate");
            this.Property(t => t.Status_Id).HasColumnName("Status_Id");

            // Relationships
            this.HasOptional(t => t.user_profile)
                .WithMany(t => t.product_gps)
                .HasForeignKey(d => d.BadBy);
            this.HasRequired(t => t.user_profile1)
                .WithMany(t => t.product_gps1)
                .HasForeignKey(d => d.CreateBy);
            this.HasOptional(t => t.user_profile2)
                .WithMany(t => t.product_gps2)
                .HasForeignKey(d => d.InstallBy);
            this.HasOptional(t => t.user_profile3)
                .WithMany(t => t.product_gps3)
                .HasForeignKey(d => d.QCBy);
            this.HasOptional(t => t.sim_brand)
                .WithMany(t => t.product_gps)
                .HasForeignKey(d => d.SIM_Brand_Id);
            this.HasOptional(t => t.sim_payment_type)
                .WithMany(t => t.product_gps)
                .HasForeignKey(d => d.SIM_Payment_Type_Id);
            this.HasOptional(t => t.product_gps_type)
                .WithMany(t => t.product_gps)
                .HasForeignKey(d => d.Status_Id);
            this.HasOptional(t => t.user_profile4)
                .WithMany(t => t.product_gps4)
                .HasForeignKey(d => d.StockBy);
            this.HasOptional(t => t.user_profile5)
                .WithMany(t => t.product_gps5)
                .HasForeignKey(d => d.UnuseableBy);
            this.HasRequired(t => t.product_gps_version)
                .WithMany(t => t.product_gps)
                .HasForeignKey(d => d.Version);

        }
    }
}
