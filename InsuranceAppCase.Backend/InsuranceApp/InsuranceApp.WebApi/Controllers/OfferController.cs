using InsuranceApp.Core.DTO_s;
using InsuranceApp.Business.Services.Interfaces;
using InsuranceApp.Core.Entities;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Xml.Serialization;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;
using Serilog;
using System;

namespace InsuranceApp.WebApi.Controllers
{
    public class OfferController : ApiController
    {
        private readonly IOfferService _offerService;

        public OfferController(IOfferService offerService)
        {
            _offerService = offerService;
        }

        public async Task<IHttpActionResult> AddAsyncFile(HttpPostedFileBase file)
        {
            return Ok();
        }
        [HttpPost]
        [Route("offer")]
        public async Task<IHttpActionResult> OfferRequest(OfferRequestDto dto)
        {
            Log.Information("Poliçe arama isteği: {@Input} UserId:{UserId} Zaman:{Time}", dto, User.Identity.Name, DateTime.Now);

            var result = await _offerService.GetOffer(dto);

            if (!result.Success && result.Data is null)
            {
                return BadRequest();
            }

            Log.Information("Poliçe arama sonucu: {ResultCount} kayıt bulundu. UserId:{UserId}", result.Data.Count, User.Identity.Name);

            return Ok(result);
        }
    }
}
