using gtrackProject.Repositories.account;
using gtrackProject.Repositories.account.IRepos;
using gtrackProject.Repositories.driver;
using gtrackProject.Repositories.driver.IRepos;
using gtrackProject.Repositories.order;
using gtrackProject.Repositories.order.IRepos;
using gtrackProject.Repositories.product;
using gtrackProject.Repositories.product.IRepos;
using gtrackProject.Repositories.universe;
using gtrackProject.Repositories.universe.IRepos;
using gtrackProject.Repositories.vehicle;
using gtrackProject.Repositories.vehicle.IRepos;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace gtrackProject
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();

            //Repositories.account
            container.RegisterType<IEmployeeAdminRepository, EmployeeAdminRepository>();
            container.RegisterType<IRoleAdminRepository, RoleAdminRepository>();
            container.RegisterType<IHdRepository, HdReository>();
            container.RegisterType<ICustomerRepository, CustomerRepository>();

            //Repositories.product
            container.RegisterType<ISimNetworkRepository, SimNetworkRepository>();
            container.RegisterType<ISimFeeTypeRepository, SimFeeTypeRepository>();
            container.RegisterType<IGpsVersionRepository, GpsVersionRepository>();
            container.RegisterType<IGpsStateRepository, ProdGpsStateRepository
            container.RegisterType<IProductGpsRepository, ProductGpsRepository>();

            //Repositories.vehicle
            container.RegisterType<ILpTypeRepository, LpTypeRepository>();
            container.RegisterType<IProvinceRepository, ProvinceRepository>();
            container.RegisterType<IVehicleColorRepository, VehicleColorRepository>();
            container.RegisterType<IVehicleOganizeRepository, VehicleOganizeRepository>();
            container.RegisterType<IVehicleHeadTypeRepository, VehicleHeadTypeRepository>();
            container.RegisterType<IVehicleTypeRepository, VehicleTypeRepository>();
            container.RegisterType<IVehicleBrandRepository, VehicleBrandRepository>();
            container.RegisterType<IVehicleModelRepository, VehicleModelRepository>();
            container.RegisterType<IVehicleRepository, VehicleRepository>();

            //Repositories.universe
            container.RegisterType<IUnCmBattRepository, UnCmBattRepository>();
            container.RegisterType<IUnCmCommRepository, UnCmCommRepository>();
            container.RegisterType<IUnCmEngineRepository, UnCmEngineRepository>();
            container.RegisterType<IUnCmGpsRepository, UnCmGpsRepository>();
            container.RegisterType<IUnCmMeterRepository, UnCmMeterRepository>();
            container.RegisterType<IUnCmSignalRepository, UnCmSignalRepository>();
            container.RegisterType<IUnCmTempRepository, UnCmTempRepository>();
            container.RegisterType<IUnDisplayRepository, UnDisplayRepository>();
            container.RegisterType<IUniverseRepository, UniverseRepository>();

            //Repositories.order
            container.RegisterType<IOdStateRepository, OdStateRepository>();
            container.RegisterType<IFixOrderRepository, FixOrderRepository>();
            container.RegisterType<IOrderRepository, OrderRepository>();

            //Repositories.order
            container.RegisterType<IDriverCateRepository, DriverCateRepository>();
            container.RegisterType<IDriverRepository, DriverRepository>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}