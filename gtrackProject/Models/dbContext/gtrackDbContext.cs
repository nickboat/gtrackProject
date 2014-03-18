using System.Data.Entity;
using gtrackProject.Models.account;
using gtrackProject.Models.driver;
using gtrackProject.Models.Mapping;
using gtrackProject.Models.Mapping.account;
using gtrackProject.Models.Mapping.driver;
using gtrackProject.Models.Mapping.order;
using gtrackProject.Models.Mapping.product;
using gtrackProject.Models.Mapping.universe;
using gtrackProject.Models.Mapping.vehicle;
using gtrackProject.Models.order;
using gtrackProject.Models.product;
using gtrackProject.Models.universe;
using gtrackProject.Models.vehicle;

namespace gtrackProject.Models.dbContext
{
    public class GtrackDbContext : DbContext
    {
        static GtrackDbContext()
        {
            Database.SetInitializer<GtrackDbContext>(null);
        }

        public GtrackDbContext()
            : base("Name=gtrackDbContext")
        {
            Configuration.LazyLoadingEnabled = false;
        }
        //account
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Hd> Hds { get; set; }
        public DbSet<UserConfig> UserConfigs { get; set; }
        //driver
        public DbSet<DriverCategory> DriverCategory { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<LogCardReader> LogCardReaders { get; set; }
        //order
        public DbSet<FixOrder> FixOrders { get; set; }
        public DbSet<OrderState> OrderStates { get; set; }
        public DbSet<Order> Orders { get; set; }
        //product
        public DbSet<Gps> Gpss { get; set; }
        public DbSet<GpsState> GpsStates { get; set; }
        public DbSet<GpsVersion> GpsVersions { get; set; }
        public DbSet<LogDelete> LogDeletes { get; set; }
        public DbSet<LogFee> LogFees { get; set; }
        public DbSet<LogMoveGps> LogMoveGpses { get; set; }
        public DbSet<LogSim> LogSims { get; set; }
        public DbSet<LogStatus> LogStatuses { get; set; }
        public DbSet<LogSwap> LogSwaps { get; set; }
        public DbSet<Sim> Sims { get; set; }
        public DbSet<SimStatus> SimStatuses { get; set; }
        public DbSet<SimNetwork> SimNetworks { get; set; }
        public DbSet<SimFeeType> SimFeeTypes { get; set; }
        //universe
        public DbSet<UnCmBatt> UnCmBatts { get; set; }
        public DbSet<UnCmComm> UnCmComms { get; set; }
        public DbSet<UnCmEngine> UnCmEngines { get; set; }
        public DbSet<UnCmGps> UnCmGpss { get; set; }
        public DbSet<UnCmMeter> UnCmMeters { get; set; }
        public DbSet<UnCmSignal> UnCmSignals { get; set; }
        public DbSet<UnCmTemp> UnCmTemps { get; set; }
        public DbSet<UnDisplayStatus> UnDisplayStatuss { get; set; }
        public DbSet<Universe> Universes { get; set; }
        //vehicle
        public DbSet<LogMoveVehicle> LogMoveVehicles { get; set; }
        public DbSet<VehicleBrand> VehicleBrands { get; set; }
        public DbSet<VehicleColor> VehicleColors { get; set; }
        public DbSet<VehicleHeadType> VehicleHeadTypes { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }
        public DbSet<VehicleOganize> VehicleOganizes { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Province> Provincess { get; set; }
        public DbSet<LpType> LpTypes { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //account
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new UserConfigMap());
            modelBuilder.Configurations.Add(new HdMap());
            modelBuilder.Configurations.Add(new EmployeeMap());
            //driver
            modelBuilder.Configurations.Add(new DriverCategoryMap());
            modelBuilder.Configurations.Add(new DriverMap());
            modelBuilder.Configurations.Add(new LogCardReaderMap());
            //order
            modelBuilder.Configurations.Add(new FixOrderMap());
            modelBuilder.Configurations.Add(new OrderStateMap());
            modelBuilder.Configurations.Add(new OrderMap());
            //product
            modelBuilder.Configurations.Add(new GpsMap());
            modelBuilder.Configurations.Add(new GpsStateMap());
            modelBuilder.Configurations.Add(new GpsVersionMap());
            modelBuilder.Configurations.Add(new LogDeleteMap());
            modelBuilder.Configurations.Add(new LogFeeMap());
            modelBuilder.Configurations.Add(new LogMoveGpsMap());
            modelBuilder.Configurations.Add(new LogSimMap());
            modelBuilder.Configurations.Add(new LogStatusMap());
            modelBuilder.Configurations.Add(new LogSwapMap());
            modelBuilder.Configurations.Add(new SimMap());
            modelBuilder.Configurations.Add(new SimStatusMap());
            modelBuilder.Configurations.Add(new SimNetworkMap());
            modelBuilder.Configurations.Add(new SimFeeTypeMap());
            //universe
            modelBuilder.Configurations.Add(new UnCmBattMap());
            modelBuilder.Configurations.Add(new UnCmCommMap());
            modelBuilder.Configurations.Add(new UnCmEngineMap());
            modelBuilder.Configurations.Add(new UnCmGpsMap());
            modelBuilder.Configurations.Add(new UnCmMeterMap());
            modelBuilder.Configurations.Add(new UnCmSignalMap());
            modelBuilder.Configurations.Add(new UnCmTempMap());
            modelBuilder.Configurations.Add(new UnDisplayStatusMap());
            modelBuilder.Configurations.Add(new UniverseMap());
            //vehicle
            modelBuilder.Configurations.Add(new LogMoveVehicleMap());
            modelBuilder.Configurations.Add(new VehicleBrandMap());
            modelBuilder.Configurations.Add(new VehicleColorMap());
            modelBuilder.Configurations.Add(new VehicleHeadTypeMap());
            modelBuilder.Configurations.Add(new VehicleModelMap());
            modelBuilder.Configurations.Add(new VehicleOganizeMap());
            modelBuilder.Configurations.Add(new VehicleTypeMap());
            modelBuilder.Configurations.Add(new VehicleMap());
            modelBuilder.Configurations.Add(new LpTypeMap());
            modelBuilder.Configurations.Add(new ProvinceMap());
        }
    }
}
