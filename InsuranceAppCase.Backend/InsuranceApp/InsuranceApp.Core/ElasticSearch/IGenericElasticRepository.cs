using InsuranceApp.Core.Interfaces;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApp.Core.ElasticSearch
{
    public interface IGenericElasticRepository<T> where T : class
    {
        Task IndexAsync(T document, string id = null);
        Task<T> GetByIdAsync(string id);
        Task<IEnumerable<T>> SearchAsync(Func<QueryContainerDescriptor<T>, QueryContainer> query);
        Task UpdateAsync(string id, T document);
        Task DeleteAsync(string id);
    }
}
