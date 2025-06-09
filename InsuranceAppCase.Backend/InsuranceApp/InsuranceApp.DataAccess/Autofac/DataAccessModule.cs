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
using InsuranceApp.DataAccess.Persistance.UnitofWork;
using System.Data.Entity;
using InsuranceApp.Core.UnitofWork;
using InsuranceApp.DataAccess.Persistance.DbConnection;

namespace InsuranceApp.DataAccess.Autofac
{
    public class DataAccessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EfCustomerDal>().As<IEfCustomerDal>().InstancePerLifetimeScope();
            builder.RegisterType<EfUnitOfWork<InsuranceAppDbContext>>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<EfOfferDal>().As<IEfOfferDal>().InstancePerLifetimeScope();
            builder.RegisterType<EfHealthPolicyDal>().As<IEfHealthPolicyDal>().InstancePerLifetimeScope();
            builder.RegisterType<EfRuleDal>().As<IEfRuleDal>().InstancePerLifetimeScope();

        }
    }
}
