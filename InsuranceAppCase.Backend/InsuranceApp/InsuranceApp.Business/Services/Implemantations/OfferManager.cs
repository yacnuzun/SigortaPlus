using InsuranceApp.Core.DTO_s;
using InsuranceApp.Business.Services.Interfaces;
using InsuranceApp.Core.EntityFramework;
using InsuranceApp.Core.Helpers.ResponseModels;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using InsuranceApp.Core.UnitofWork;
using InsuranceApp.Core.Entities;

namespace InsuranceApp.Business
{
    public class OfferManager : IOfferService
    {
        private readonly IEfOfferDal _efOfferDal;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<OfferManager> _logger;

        public OfferManager(IEfOfferDal efOfferDal, ILogger<OfferManager> logger, IUnitOfWork unitOfWork)
        {
            _efOfferDal = efOfferDal;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<IDataResult<List<OfferResponseDto>>> GetOffer(OfferRequestDto dto)
        {
            try
            {
                var result = _efOfferDal.GetMatchingHealthPolicies(dto);
                return new SuccessDataResult<List<OfferResponseDto>>(result);
            }
            catch (System.Exception)
            {
                _logger.LogError("Teklif sürecinde bir hata oldu.");
                throw;
            }
            
        }
    }
}
