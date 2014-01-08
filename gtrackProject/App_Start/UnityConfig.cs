using gtrackProject.Repositories.account;
using gtrackProject.Repositories.account.IRepos;
using gtrackProject.Repositories.product;
using gtrackProject.Repositories.product.IRepos;
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
            container.RegisterType<ISimBrandRepository, SimBrandRepository>();
            container.RegisterType<ISimPaymentRepository, SimPaymentRepository>();
            container.RegisterType<IGpsVersionRepository, GpsVersionRepository>();
            container.RegisterType<IGpsTypeRepository, GpsTypeRepository>();
            container.RegisterType<ICameraRepository, CameraRepository>();
            container.RegisterType<IProductGpsRepository, ProductGpsRepository>();

            //Repositories.vehicle
            container.RegisterType<ILpTypeRepository, LpTypeRepository>();
            container.RegisterType<IProvinceRepository, ProvinceRepository>();
            container.RegisterType<IVehicleColorRepository, VehicleColorRository>();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}