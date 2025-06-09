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
using InsuranceApp.Core.Autofac;

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

            // Repository, service, controller vs.ApplicationModule
            builder.RegisterModule<DataAccessModule>();
            builder.RegisterModule<ApplicationModule>();
            builder.RegisterModule<CoreModule>();

            // Controller registration
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Build container
            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;

        }
    }

}
