using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using gtrackProject.Models.Mapping;

namespace gtrackProject.Models
{
    public partial class gtrackDbContext : DbContext
    {
        static gtrackDbContext()
        {
            Database.SetInitializer<gtrackDbContext>(null);
        }

        public gtrackDbContext()
            : base("Name=gtrackDbContext")
        {

        }

        public DbSet<driver_category> driver_category { get; set; }
        public DbSet<driver> drivers { get; set; }
        public DbSet<fix_orders> fix_orders { get; set; }
        public DbSet<hd> hds { get; set; }
        public DbSet<lp_type> lp_type { get; set; }
        public DbSet<order_extend_type> order_extend_type { get; set; }
        public DbSet<order_status> order_status { get; set; }
        public DbSet<order> orders { get; set; }
        public DbSet<product_camera> product_camera { get; set; }
        public DbSet<product_gps> product_gps { get; set; }
        public DbSet<product_gps_type> product_gps_type { get; set; }
        public DbSet<product_gps_version> product_gps_version { get; set; }
        public DbSet<province> provinces { get; set; }
        public DbSet<sim_brand> sim_brand { get; set; }
        public DbSet<sim_payment_type> sim_payment_type { get; set; }
        public DbSet<un_cm_batt> un_cm_batt { get; set; }
        public DbSet<un_cm_comm> un_cm_comm { get; set; }
        public DbSet<un_cm_engine> un_cm_engine { get; set; }
        public DbSet<un_cm_gps> un_cm_gps { get; set; }
        public DbSet<un_cm_meter> un_cm_meter { get; set; }
        public DbSet<un_cm_signal> un_cm_signal { get; set; }
        public DbSet<un_cm_temp> un_cm_temp { get; set; }
        public DbSet<un_display_status> un_display_status { get; set; }
        public DbSet<universe> universes { get; set; }
        public DbSet<user_profile> user_profile { get; set; }
        public DbSet<vehicle_brand> vehicle_brand { get; set; }
        public DbSet<vehicle_color> vehicle_color { get; set; }
        public DbSet<vehicle_head_type> vehicle_head_type { get; set; }
        public DbSet<vehicle_model> vehicle_model { get; set; }
        public DbSet<vehicle_oganize> vehicle_oganize { get; set; }
        public DbSet<vehicle_type> vehicle_type { get; set; }
        public DbSet<vehicle> vehicles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new driver_categoryMap());
            modelBuilder.Configurations.Add(new driverMap());
            modelBuilder.Configurations.Add(new fix_ordersMap());
            modelBuilder.Configurations.Add(new hdMap());
            modelBuilder.Configurations.Add(new lp_typeMap());
            modelBuilder.Configurations.Add(new order_extend_typeMap());
            modelBuilder.Configurations.Add(new order_statusMap());
            modelBuilder.Configurations.Add(new orderMap());
            modelBuilder.Configurations.Add(new product_cameraMap());
            modelBuilder.Configurations.Add(new product_gpsMap());
            modelBuilder.Configurations.Add(new product_gps_typeMap());
            modelBuilder.Configurations.Add(new product_gps_versionMap());
            modelBuilder.Configurations.Add(new provinceMap());
            modelBuilder.Configurations.Add(new sim_brandMap());
            modelBuilder.Configurations.Add(new sim_payment_typeMap());
            modelBuilder.Configurations.Add(new un_cm_battMap());
            modelBuilder.Configurations.Add(new un_cm_commMap());
            modelBuilder.Configurations.Add(new un_cm_engineMap());
            modelBuilder.Configurations.Add(new un_cm_gpsMap());
            modelBuilder.Configurations.Add(new un_cm_meterMap());
            modelBuilder.Configurations.Add(new un_cm_signalMap());
            modelBuilder.Configurations.Add(new un_cm_tempMap());
            modelBuilder.Configurations.Add(new un_display_statusMap());
            modelBuilder.Configurations.Add(new universeMap());
            modelBuilder.Configurations.Add(new user_profileMap());
            modelBuilder.Configurations.Add(new vehicle_brandMap());
            modelBuilder.Configurations.Add(new vehicle_colorMap());
            modelBuilder.Configurations.Add(new vehicle_head_typeMap());
            modelBuilder.Configurations.Add(new vehicle_modelMap());
            modelBuilder.Configurations.Add(new vehicle_oganizeMap());
            modelBuilder.Configurations.Add(new vehicle_typeMap());
            modelBuilder.Configurations.Add(new vehicleMap());
        }
    }
}
