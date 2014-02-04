﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using gtrackProject.Models.product;

namespace gtrackProject.Models.Mapping.product
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductCameraStatusMap : EntityTypeConfiguration<ProductCameraStatus>
    {
        public ProductCameraStatusMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Val)
                .IsRequired()
                .HasMaxLength(1);

            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            ToTable("product_camera_status");
            Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.Val).HasColumnName("Val");
        }
    }
}