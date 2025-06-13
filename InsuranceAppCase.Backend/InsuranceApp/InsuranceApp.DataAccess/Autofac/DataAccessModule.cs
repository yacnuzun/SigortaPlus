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
using InsuranceApp.DataAccess.Persistance.ElasticSearch;
using InsuranceApp.Core.ElasticSearch;
using InsuranceApp.Core.Interfaces;
using Nest;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using Elastic.Clients.Elasticsearch.TransformManagement;
using InsuranceApp.Core.Entities;

namespace InsuranceApp.DataAccess.Autofac
{
    public class DataAccessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region elasticSearch
            Log.Logger = new LoggerConfiguration()
            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("https://localhost:9200"))
            {
                AutoRegisterTemplate = true,
                IndexFormat = "insuranceapp-logs-{0:yyyy.MM.dd}",
                ModifyConnectionSettings = c =>
                c.ServerCertificateValidationCallback((o, cert, chain, errors) => true)
                    .BasicAuthentication("elastic", "+BuM-6tassAeNx8fPqJi")
            })
            .WriteTo.Console()
            .WriteTo.File("C:\\Users\\user\\Desktop\\yalcin\\InsuranceAppCase\\InsuranceAppCase.Backend\\InsuranceApp\\insuranceapp-logs.txt")
            .CreateLogger();

            Log.Information("Serilog started");

            // ElasticClient ayarları
            var elasticSettings = new ConnectionSettings(new Uri("https://localhost:9200"))
                .ServerCertificateValidationCallback((o, certificate, chain, errors) => true)
                .BasicAuthentication("elastic", "+BuM-6tassAeNx8fPqJi")
                .DefaultIndex("insuranceapp-logs");

            var elasticClient = new ElasticClient(elasticSettings);

            // Bağlantı testi: ES ayakta mı? Sonucu logla!
            try
            {
                var pingResponse = elasticClient.Ping();
                if (pingResponse.IsValid)
                    Log.Information("ElasticSearch bağlantısı başarılı!");
                else
                    Log.Error("ElasticSearch bağlantı hatası: {@PingDebug}", pingResponse.DebugInformation);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "ElasticSearch bağlantı sırasında istisna oluştu!");
            }
            #endregion

            var migrator = new ElasticIndexMigrator(elasticClient);
            #region elasticindex
            migrator.MigrateIndexAsync<OfferElasticDto>("offer").GetAwaiter().GetResult();
            migrator.MigrateIndexAsync<OfferLog>("offerlog").GetAwaiter().GetResult();
            migrator.MigrateIndexAsync<CustomerElasticDto>("customer").GetAwaiter().GetResult();
            migrator.MigrateIndexAsync<CustomerLog>("customerlog").GetAwaiter().GetResult();
            migrator.MigrateIndexAsync<HealthPolicyElasticDto>("healthpolicy").GetAwaiter().GetResult();
            migrator.MigrateIndexAsync<HealthPolicyLog>("healthpolicy").GetAwaiter().GetResult();
            migrator.MigrateIndexAsync<RuleElasticDto>("rule").GetAwaiter().GetResult();
            migrator.MigrateIndexAsync<RuleLog>("rulelog").GetAwaiter().GetResult();
            migrator.MigrateIndexAsync<OfferHealthPolicyElaticDto>("offerhealthpolicy").GetAwaiter().GetResult();
            migrator.MigrateIndexAsync<OfferHealthPolicyLog>("offerhealthpolicylog").GetAwaiter().GetResult();
            migrator.MigrateIndexAsync<BaseElasticSearchEntity>("searchentity").GetAwaiter().GetResult();
            #endregion
            // DI kayıtları
            builder.RegisterInstance(new ElasticClient(elasticSettings)).As<IElasticClient>().SingleInstance();

            builder.RegisterType<EfCustomerDal>().As<IEfCustomerDal>().InstancePerLifetimeScope();
            builder.RegisterType<EfUnitOfWork<InsuranceAppDbContext>>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<EfOfferDal>().As<IEfOfferDal>().InstancePerLifetimeScope();
            builder.RegisterType<EfOfferHealthPolicyDal>().As<IEfOfferHealthPolicyDal>().InstancePerLifetimeScope();
            builder.RegisterType<EfHealthPolicyDal>().As<IEfHealthPolicyDal>().InstancePerLifetimeScope();
            builder.RegisterType<EfRuleDal>().As<IEfRuleDal>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(GenericElasticRepository<>))
            .As(typeof(IGenericElasticRepository<>))
            .InstancePerLifetimeScope();
        }
    }
}
