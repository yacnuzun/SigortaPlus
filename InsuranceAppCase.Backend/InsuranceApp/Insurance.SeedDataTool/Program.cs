using Autofac;
using Insurance.SeedDataTool.Autofac;
using InsuranceApp.Business;
using InsuranceApp.Business.Services.Interfaces;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.SeedDataTool
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            var container = AutofacConsoleConfig.Configure();

            var scope = container.BeginLifetimeScope();

            //var fakeDataService = scope.Resolve<IFakeDataService>();

            //var result = fakeDataService.GenerateAsync().Result;

            //Console.WriteLine(result.Message ?? "Fake data oluşturuldu.");
        }
    }
}
