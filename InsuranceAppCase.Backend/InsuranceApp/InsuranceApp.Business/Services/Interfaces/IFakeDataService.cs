using InsuranceApp.Core.Helpers.ResponseModels;
using System.Threading.Tasks;

namespace InsuranceApp.Business.Services.Interfaces
{
    public interface IFakeDataService
    {
        Task<IResult> GenerateAsync();
    }
}
