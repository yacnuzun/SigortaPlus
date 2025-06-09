using InsuranceApp.Core.DTO_s;
using InsuranceApp.Core.Helpers.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceApp.Business.Services.Interfaces
{
    public interface IOfferService
    {
        Task<IDataResult<List<OfferResponseDto>>> GetOffer(OfferRequestDto dto);
    }
}
