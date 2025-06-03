using InsuranceApp.Business.DTO_s;
using InsuranceApp.Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace InsuranceApp.WebApi.Controllers
{
    public class CustomerController : ApiController
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("getcustomer")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var response = await _service.GetCustomer(id);
            return Ok(response);
        }

        [HttpGet]
        [Route("getcustomers")]
        public async Task<IHttpActionResult> GetAll()
        {
            var response = await _service.GetCustomers();
            return Ok(response);
        }

        public async Task<IHttpActionResult> Add(CustomerCreateDto customerCreateDto)
        {
            _service.Add(customerCreateDto);
            return Ok();
        }
    }

    public class OfferController: ApiController
    {
        private readonly IOfferService _offerService;

        public OfferController(IOfferService offerService)
        {
            _offerService = offerService;
        }

        public async Task<IHttpActionResult> AddAsyncFile(IFormFile file)
        {

        }
    }
}
