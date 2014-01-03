using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using gtrackProject.Models.account;
using gtrackProject.Models.Mapping;

namespace gtrackProject.Models
{
    public partial class GtrackDbContext : DbContext
    {
        static GtrackDbContext()
        {
            Database.SetInitializer<GtrackDbContext>(null);
        }

        public GtrackDbContext()
            : base("Name=gtrackDbContext")
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<DriverCategory> DriverCategory { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<FixOrders> FixOrders { get; set; }
        public DbSet<Hd> Hds { get; set; }
        public DbSet<LpType> LpTypes { get; set; }
        public DbSet<OrderExtendType> OrderExtendTypes { get; set; }
        public DbSet<OrderStatus> OrderStatuss { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductCamera> ProductCameras { get; set; }
        public DbSet<ProductGps> ProductGpss { get; set; }
        public DbSet<ProductGpsType> ProductGpsTypes { get; set; }
        public DbSet<ProductGpsVersion> ProductGpsVersions { get; set; }
        public DbSet<Province> Provincess { get; set; }
        public DbSet<SimBrand> SimBrands { get; set; }
        public DbSet<SimPaymentType> SimPaymentTypes { get; set; }
        public DbSet<UnCmBatt> UnCmBatts { get; set; }
        public DbSet<UnCmComm> UnCmComms { get; set; }
        public DbSet<UnCmEngine> UnCmEngines { get; set; }
        public DbSet<UnCmGps> UnCmGpss { get; set; }
        public DbSet<UnCmMeter> UnCmMeters { get; set; }
        public DbSet<UnCmSignal> UnCmSignals { get; set; }
        public DbSet<UnCmTemp> UnCmTemps { get; set; }
        public DbSet<UnDisplayStatus> UnDisplayStatuss { get; set; }
        public DbSet<Universe> Universes { get; set; }
        public DbSet<VehicleBrand> VehicleBrands { get; set; }
        public DbSet<VehicleColor> VehicleColors { get; set; }
        public DbSet<VehicleHeadType> VehicleHeadTypes { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }
        public DbSet<VehicleOganize> VehicleOganizes { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new DriverCategoryMap());
            modelBuilder.Configurations.Add(new DriverMap());
            modelBuilder.Configurations.Add(new EmployeeMap());
            modelBuilder.Configurations.Add(new FixOrdersMap());
            modelBuilder.Configurations.Add(new HdMap());
            modelBuilder.Configurations.Add(new LpTypeMap());
            modelBuilder.Configurations.Add(new OrderExtendTypeMap());
            modelBuilder.Configurations.Add(new OrderStatusMap());
            modelBuilder.Configurations.Add(new OrderMap());
            modelBuilder.Configurations.Add(new ProductCameraMap());
            modelBuilder.Configurations.Add(new ProductGpsMap());
            modelBuilder.Configurations.Add(new ProductGpsTypeMap());
            modelBuilder.Configurations.Add(new ProductGpsVersionMap());
            modelBuilder.Configurations.Add(new ProvinceMap());
            modelBuilder.Configurations.Add(new SimBrandMap());
            modelBuilder.Configurations.Add(new SimPaymentTypeMap());
            modelBuilder.Configurations.Add(new UnCmBattMap());
            modelBuilder.Configurations.Add(new UnCmCommMap());
            modelBuilder.Configurations.Add(new UnCmEngineMap());
            modelBuilder.Configurations.Add(new UnCmGpsMap());
            modelBuilder.Configurations.Add(new UnCmMeterMap());
            modelBuilder.Configurations.Add(new UnCmSignalMap());
            modelBuilder.Configurations.Add(new UnCmTempMap());
            modelBuilder.Configurations.Add(new UnDisplayStatusMap());
            modelBuilder.Configurations.Add(new UniverseMap());
            modelBuilder.Configurations.Add(new VehicleBrandMap());
            modelBuilder.Configurations.Add(new VehicleColorMap());
            modelBuilder.Configurations.Add(new VehicleHeadTypeMap());
            modelBuilder.Configurations.Add(new VehicleModelMap());
            modelBuilder.Configurations.Add(new VehicleOganizeMap());
            modelBuilder.Configurations.Add(new VehicleTypeMap());
            modelBuilder.Configurations.Add(new VehicleMap());
        }
    }
}
