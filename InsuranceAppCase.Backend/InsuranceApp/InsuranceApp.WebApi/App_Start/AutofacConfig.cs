using Autofac.Integration.WebApi;
using Autofac;
using System.Reflection;
using System.Web.Http;
using InsuranceApp.Business.Autofac;
using InsuranceApp.DataAccess.Autofac;
using InsuranceApp.Core.EntityFramework;
using InsuranceApp.DataAccess.Persistance.Repositories;
using InsuranceApp.Business;
using InsuranceApp.Business.Services.Interfaces;
using InsuranceApp.DataAccess.Persistance.DbConnection;

namespace InsuranceApp.WebApi
{
    public class AutofacConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            // DbContext registration (EF6 için)
            builder.RegisterType<InsuranceAppDbContext>()
                   .AsSelf()
                   .InstancePerRequest(); // Web API'de her istek için ayrı context

            // Repository, service, controller vs.
            builder.RegisterType<EfCustomerDal>().As<IEfCustomerDal>();
            builder.RegisterType<CustomerManager>().As<ICustomerService>();

            // Controller registration
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Build container
            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;

        }
    }

}
