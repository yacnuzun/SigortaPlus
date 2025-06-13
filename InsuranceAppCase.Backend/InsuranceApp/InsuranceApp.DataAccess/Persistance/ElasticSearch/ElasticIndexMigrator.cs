using InsuranceApp.Core.Entities;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApp.DataAccess.Persistance.ElasticSearch
{
    public class ElasticIndexMigrator
    {
        private readonly IElasticClient _client;

        public ElasticIndexMigrator(IElasticClient client)
        {
            _client = client;
        }

        public async Task MigrateIndexAsync<T>(string indexName) where T : class 
        {
            var exists = await _client.Indices.ExistsAsync(indexName);
            if (!exists.Exists)
            {
                var createResponse = await _client.Indices.CreateAsync(indexName, c => c
                    .Map(m => m.AutoMap<T>())
                );
                if (!createResponse.IsValid)
                    throw new Exception($"Index migration failed: {createResponse.DebugInformation}");
            }
        }
    }
}
