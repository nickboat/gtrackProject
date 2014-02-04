using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using gtrackProject.Models.product;

namespace gtrackProject.Models.Mapping.product
{
    public class ProductGpsMap : EntityTypeConfiguration<ProductGps>
    {
        public ProductGpsMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.SimNum)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            Property(t => t.Serial)
                .IsRequired()
                .HasMaxLength(10);

            Property(t => t.ErrProductComment)
                .HasMaxLength(150);

            Property(t => t.BadComment)
                .HasMaxLength(150);

            Property(t => t.UnuseableComment)
                .HasMaxLength(150);

            // Table & Column Mappings
            ToTable("product_gps");
            Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.SimNum).HasColumnName("SIM_Num");
            Property(t => t.SimBrandId).HasColumnName("SIM_Brand_Id");
            Property(t => t.SimPaymentTypeId).HasColumnName("SIM_Payment_Type_Id");
            Property(t => t.Serial).HasColumnName("Serial");
            Property(t => t.Version).HasColumnName("Version");
            Property(t => t.CreateBy).HasColumnName("CreateBy");
            Property(t => t.CreateDate).HasColumnName("CreateDate");
            Property(t => t.StockBy).HasColumnName("StockBy");
            Property(t => t.StockDate).HasColumnName("StockDate");
            Property(t => t.QcBy).HasColumnName("QCBy");
            Property(t => t.QcDate).HasColumnName("QCDate");
            Property(t => t.InstallBy).HasColumnName("InstallBy");
            Property(t => t.InstallDate).HasColumnName("InstallDate");
            Property(t => t.ErrProductComment).HasColumnName("ErrProductComment");
            Property(t => t.BadBy).HasColumnName("BadBy");
            Property(t => t.BadDate).HasColumnName("BadDate");
            Property(t => t.BadComment).HasColumnName("BadComment");
            Property(t => t.UnuseableBy).HasColumnName("UnuseableBy");
            Property(t => t.UnuseableDate).HasColumnName("UnuseableDate");
            Property(t => t.UnuseableComment).HasColumnName("UnuseableComment");
            Property(t => t.ExpireDate).HasColumnName("ExpireDate");
            Property(t => t.LastExtendDate).HasColumnName("LastExtendDate");
            Property(t => t.State).HasColumnName("State");
            Property(t => t.MemoryId).HasColumnName("Memory_Id");

            // Relationships
            HasOptional(t => t.CreateByEmployee)
                .WithMany(t => t.GpsCreates)
                .HasForeignKey(d => d.CreateBy);
            HasOptional(t => t.StockByEmployee)
                .WithMany(t => t.GpsStocks)
                .HasForeignKey(d => d.StockBy);
            HasOptional(t => t.QcByEmployee)
                .WithMany(t => t.GpsQcs)
                .HasForeignKey(d => d.QcBy);
            HasOptional(t => t.InstallByEmployee)
                .WithMany(t => t.GpsInstalls)
                .HasForeignKey(d => d.InstallBy);
            HasOptional(t => t.BadByEmployee)
                .WithMany(t => t.GpsBads)
                .HasForeignKey(d => d.BadBy);
            HasOptional(t => t.UnuseableByEmployee)
                .WithMany(t => t.GpsUnuses)
                .HasForeignKey(d => d.UnuseableBy);
            HasOptional(t => t.SimBrand)
                .WithMany(t => t.ProductGpss)
                .HasForeignKey(d => d.SimBrandId);
            HasOptional(t => t.SimPaymentType)
                .WithMany(t => t.ProductGpss)
                .HasForeignKey(d => d.SimPaymentTypeId);
            HasOptional(t => t.ProductGpsState)
                .WithMany(t => t.ProductGpss)
                .HasForeignKey(d => d.State);
            HasRequired(t => t.ProductGpsVersion)
                .WithMany(t => t.ProductGpss)
                .HasForeignKey(d => d.Version);
            HasOptional(t => t.MemoryStatus)
                .WithMany(t => t.ProductGpss)
                .HasForeignKey(d => d.MemoryId);
        }
    }
}
