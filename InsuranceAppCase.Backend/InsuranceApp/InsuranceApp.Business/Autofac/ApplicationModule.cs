using Autofac;
using InsuranceApp.Business.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Module = Autofac.Module;

namespace InsuranceApp.Business.Autofac
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CustomerManager>().As<ICustomerService>().InstancePerRequest();
            builder.RegisterType<FakeDataService>().As<IFakeDataService>().InstancePerLifetimeScope();
            builder.RegisterType<OfferManager>().As<IOfferService>();
        }
    }
}
