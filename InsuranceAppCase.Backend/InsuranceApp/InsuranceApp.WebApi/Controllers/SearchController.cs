using InsuranceApp.Business.Services.Interfaces;
using InsuranceApp.Core.DTO_s;
using InsuranceApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace InsuranceApp.WebApi.Controllers
{
    public class SearchController : ApiController
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpPost]
        public async Task<IHttpActionResult> SearchAsync(SearchRequest request)
        {
            var result = await _searchService.SmartSearch(request.SearchValue);
            return Ok(result);
        }
    }
}
