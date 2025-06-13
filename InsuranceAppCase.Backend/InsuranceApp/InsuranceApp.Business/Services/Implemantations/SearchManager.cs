using InsuranceApp.Business.Services.Interfaces;
using InsuranceApp.Core.DTO_s;
using InsuranceApp.Core.ElasticSearch;
using InsuranceApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApp.Business.Services.Implemantations
{
    public class SearchManager: ISearchService
    {
        private readonly IGenericElasticRepository<BaseElasticSearchEntity> _genericelasticRepo;
        private readonly IGenericElasticRepository<CustomerElasticDto> _genericCustomerRepo;
        private readonly IGenericElasticRepository<OfferElasticDto> _genericelasticOfferRepo;

        public SearchManager(IGenericElasticRepository<BaseElasticSearchEntity> genericelasticRepo, 
            IGenericElasticRepository<CustomerElasticDto> genericCustomerRepo, 
            IGenericElasticRepository<OfferElasticDto> genericelasticOfferRepo)
        {
            _genericelasticRepo = genericelasticRepo;
            _genericCustomerRepo = genericCustomerRepo;
            _genericelasticOfferRepo = genericelasticOfferRepo;
        }

        // Genel Akıllı Arama (BaseElasticSearchEntity üzerinden örnek)
        public async Task<IEnumerable<BaseElasticSearchEntity>> SmartSearch(string searchTerm)
        {
            var results = await _genericelasticRepo.SearchAsync(q =>
                q.MultiMatch(m => m
                    .Fields(f => f.Field(x => x.Title).Field(x => x.Description).Field(x => x.Url))
                    .Query(searchTerm)
                )
            );
            return results; // object olarak dönülüyor
        }

        // Sadece Customer için arama
        public async Task<IEnumerable<object>> SearchforCustomer(string searchTerm, params string[] indexNames)
        {
            var results = await _genericCustomerRepo.SearchAsync(q =>
                q.MultiMatch(m => m
                    .Fields(f => f.Field(x => x.Title).Field(x => x.Description).Field(x => x.FullName))
                    .Query(searchTerm)
                )
            );
            return results.Cast<object>();
        }

        // Sadece Offer için arama
        public async Task<IEnumerable<object>> SearchforOffer(string searchTerm, params string[] indexNames)
        {
            var results = await _genericelasticOfferRepo.SearchAsync(q =>
                q.MultiMatch(m => m
                    .Fields(f => f.Field(x => x.Title).Field(x => x.Description))
                    .Query(searchTerm)
                )
            );
            return results.Cast<object>();
        }
    }
}
