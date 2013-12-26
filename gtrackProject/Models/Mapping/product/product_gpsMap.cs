using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace gtrackProject.Models.Mapping
{
    public class ProductGpsMap : EntityTypeConfiguration<ProductGps>
    {
        public ProductGpsMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.SimNum)
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
            this.Property(t => t.SimNum).HasColumnName("SIM_Num");
            this.Property(t => t.SimBrandId).HasColumnName("SIM_Brand_Id");
            this.Property(t => t.SimPaymentTypeId).HasColumnName("SIM_Payment_Type_Id");
            this.Property(t => t.Serial).HasColumnName("Serial");
            this.Property(t => t.Version).HasColumnName("Version");
            this.Property(t => t.CreateBy).HasColumnName("CreateBy");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.StockBy).HasColumnName("StockBy");
            this.Property(t => t.StockDate).HasColumnName("StockDate");
            this.Property(t => t.QcBy).HasColumnName("QCBy");
            this.Property(t => t.QcDate).HasColumnName("QCDate");
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
            this.Property(t => t.StatusId).HasColumnName("Status_Id");

            // Relationships
            this.HasOptional(t => t.CreateByEmployee)
                .WithMany(t => t.GpsCreates)
                .HasForeignKey(d => d.BadBy);
            this.HasOptional(t => t.StockByEmployee)
                .WithMany(t => t.GpsStocks)
                .HasForeignKey(d => d.CreateBy);
            this.HasOptional(t => t.QcByEmployee)
                .WithMany(t => t.GpsQcs)
                .HasForeignKey(d => d.InstallBy);
            this.HasOptional(t => t.InstallByEmployee)
                .WithMany(t => t.GpsInstalls)
                .HasForeignKey(d => d.QcBy);
            this.HasOptional(t => t.BadByEmployee)
                .WithMany(t => t.GpsBads)
                .HasForeignKey(d => d.StockBy);
            this.HasOptional(t => t.UnuseableByEmployee)
                .WithMany(t => t.GpsUnuses)
                .HasForeignKey(d => d.UnuseableBy);
            this.HasOptional(t => t.SimBrand)
                .WithMany(t => t.ProductGpss)
                .HasForeignKey(d => d.SimBrandId);
            this.HasOptional(t => t.SimPaymentType)
                .WithMany(t => t.ProductGpss)
                .HasForeignKey(d => d.SimPaymentTypeId);
            this.HasOptional(t => t.ProductGpsType)
                .WithMany(t => t.ProductGpss)
                .HasForeignKey(d => d.StatusId);
            this.HasRequired(t => t.ProductGpsVersion)
                .WithMany(t => t.ProductGpss)
                .HasForeignKey(d => d.Version);

        }
    }
}
