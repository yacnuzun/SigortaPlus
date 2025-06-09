using Autofac;
using InsuranceApp.Business.Autofac;
using InsuranceApp.Core.Autofac;
using InsuranceApp.DataAccess.Autofac;
using InsuranceApp.DataAccess.Persistance.DbConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.SeedDataTool.Autofac
{
    public static class AutofacConsoleConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<InsuranceAppDbContext>()
                   .AsSelf()
                   .InstancePerLifetimeScope();
            builder.RegisterModule<CoreModule>();
            builder.RegisterModule<DataAccessModule>();
            builder.RegisterModule<ApplicationModule>();

            return builder.Build();
        }
    }

}
