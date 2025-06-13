using InsuranceApp.Core.DTO_s;
using InsuranceApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApp.Business.Services.Interfaces
{
    public interface ISearchService
    {
        Task<IEnumerable<BaseElasticSearchEntity>> SmartSearch(string searchTerm);
        Task<IEnumerable<object>> SearchforCustomer(string searchTerm, params string[] indexNames);
        Task<IEnumerable<object>> SearchforOffer(string searchTerm, params string[] indexNames);
    }
}
