using InsuranceApp.Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace InsuranceApp.WebApi.Controllers
{
    public class OfferController: ApiController
    {
        private readonly IOfferService _offerService;

        public OfferController(IOfferService offerService)
        {
            _offerService = offerService;
        }

        public async Task<IHttpActionResult> AddAsyncFile(IFormFile file)
        {
            return Ok();
        }
    }
}
