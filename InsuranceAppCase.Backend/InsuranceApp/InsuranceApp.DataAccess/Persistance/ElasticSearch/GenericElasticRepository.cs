using InsuranceApp.Core.ElasticSearch;
using InsuranceApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Npgsql.PostgresTypes.PostgresCompositeType;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Nest;
using InsuranceApp.Core.Entities;

namespace InsuranceApp.DataAccess.Persistance.ElasticSearch
{
    public class GenericElasticRepository<T> : IGenericElasticRepository<T> where T : BaseElasticSearchEntity
    {
        private readonly IElasticClient _elasticClient;
        private readonly string _indexName;

        public GenericElasticRepository(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
            _indexName = typeof(T).Name.ToLower(); // Örn: offerlog
        }

        public async Task IndexAsync(T document, string id = null)
        {
            var response = await _elasticClient.IndexAsync(document, i => i.Index(_indexName).Id((document.Id.ToString())));
            if (!response.IsValid)
            {
                // Hata detayı burada!
                throw new Exception($"Elastic Index Hatası: {response.DebugInformation}");
            }
        }

        public async Task IndexRangeAsync(IEnumerable<T> document)
        {
            var response = await _elasticClient.BulkAsync(i => i.IndexMany(document).Index(_indexName));
            if (!response.IsValid)
            {
                // Hata detayı burada!
                throw new Exception($"Elastic Index Hatası: {response.DebugInformation}");
            }
        }

        public async Task<T> GetByIdAsync(string id)
        {
            var response = await _elasticClient.GetAsync<T>(id, g => g.Index(_indexName));
            return response.Source;
        }

        public async Task<IEnumerable<T>> SearchAsync(Func<QueryContainerDescriptor<T>, QueryContainer> query)
        {
            var response = await _elasticClient.SearchAsync<T>(s => s.Index(_indexName).Query(query));
            return response.Documents;
        }

        public async Task UpdateAsync(string id, T document)
        {
            await _elasticClient.UpdateAsync<T>(id, u => u.Index(_indexName).Doc(document));
        }

        public async Task DeleteAsync(string id)
        {
            var response = await _elasticClient.DeleteAsync<T>(id, d => d.Index(_indexName));
            if (!response.IsValid)
            {
                // Hata detayı burada!
                throw new Exception($"Elastic Index Hatası: {response.DebugInformation}");
            }
        }
    }

}
