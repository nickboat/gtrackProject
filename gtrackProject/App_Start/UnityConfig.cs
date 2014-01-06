using gtrackProject.Repositories.account;
using gtrackProject.Repositories.product;
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
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}