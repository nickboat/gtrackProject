using System;
using System.Collections.Generic;

namespace gtrackProject.Models
{
    public sealed partial class ProductGps
    {
        public ProductGps()
        {
            this.Cameras = new List<ProductCamera>();
            this.Universes = new List<Universe>();
        }

        public int Id { get; set; }
        public string SimNum { get; set; }
        public byte? SimBrandId { get; set; }
        public byte? SimPaymentTypeId { get; set; }
        public string Serial { get; set; }
        public byte Version { get; set; }
        public int? CreateBy { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int? StockBy { get; set; }
        public DateTime? StockDate { get; set; }
        public int? QcBy { get; set; }
        public DateTime? QcDate { get; set; }
        public int? InstallBy { get; set; }
        public DateTime? InstallDate { get; set; }
        public string ErrProductComment { get; set; }
        public int? BadBy { get; set; }
        public DateTime? BadDate { get; set; }
        public string BadComment { get; set; }
        public int? UnuseableBy { get; set; }
        public DateTime? UnuseableDate { get; set; }
        public string UnuseableComment { get; set; }
        public DateTime? ExpireDate { get; set; }
        public DateTime? LastExtendDate { get; set; }
        public byte? StatusId { get; set; }
        public Employee CreateByEmployee { get; set; }
        public Employee StockByEmployee { get; set; }
        public Employee QcByEmployee { get; set; }
        public Employee InstallByEmployee { get; set; }
        public Employee BadByEmployee { get; set; }
        public Employee UnuseableByEmployee { get; set; }
        public ICollection<ProductCamera> Cameras { get; set; }
        public SimBrand SimBrand { get; set; }
        public SimPaymentType SimPaymentType { get; set; }
        public ProductGpsType ProductGpsType { get; set; }
        public ProductGpsVersion ProductGpsVersion { get; set; }
        public ICollection<Universe> Universes { get; set; }
    }
}
