using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using InsuranceApp.Core.Helpers.Faker;
using InsuranceApp.Core.Helpers.FileHelper;

namespace InsuranceApp.Core.Autofac
{
    public class CoreModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FileHelper>().As<IFileHelper>();
            builder.RegisterType<FakerService>().As<IFakerService>();
        }
    }
}
