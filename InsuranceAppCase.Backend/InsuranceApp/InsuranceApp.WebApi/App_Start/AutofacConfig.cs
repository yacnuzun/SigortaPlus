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
using Microsoft.Extensions.Logging;

namespace InsuranceApp.WebApi
{
    public class AutofacConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            var loggerFactory = LoggerFactory.Create(b =>
            {
                b.AddConsole(); // veya başka bir provider ekle
            });

            // DbContext registration (EF6 için)
            builder.RegisterType<InsuranceAppDbContext>()
                   .AsSelf()
                   .InstancePerRequest(); // Web API'de her istek için ayrı context



            builder.RegisterInstance(loggerFactory).As<ILoggerFactory>().SingleInstance();
            builder.RegisterGeneric(typeof(Logger<>)).As(typeof(ILogger<>)).SingleInstance();


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
