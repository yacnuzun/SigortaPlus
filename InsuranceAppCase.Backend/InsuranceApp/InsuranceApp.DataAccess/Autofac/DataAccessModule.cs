using InsuranceApp.Business.Services.Interfaces;
using InsuranceApp.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Module = Autofac.Module;
using InsuranceApp.DataAccess.Persistance.Repositories;
using InsuranceApp.Core.EntityFramework;

namespace InsuranceApp.DataAccess.Autofac
{
    public class DataAccessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EfCustomerDal>().As<IEfCustomerDal>().InstancePerRequest();
            //builder.RegisterType<OfferService>().As<IOfferService>().InstancePerRequest();
        }
    }
}
