using Bogus;
using InsuranceApp.Business.Services.Interfaces;
using InsuranceApp.Core.Entities;
using InsuranceApp.Core.Helpers.Faker;
using InsuranceApp.Core.Helpers.ResponseModels;
using InsuranceApp.Core.UnitofWork;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceApp.Business
{
    public class FakeDataService : IFakeDataService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFakerService _faker;

        public FakeDataService(IUnitOfWork unitOfWork, IFakerService faker)
        {
            _unitOfWork = unitOfWork;
            _faker = faker;
        }

        public async Task<IResult> GenerateAsync()
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var policies = _faker.GenerateHealthPolicies(10);
                var entitiesPolicy = await _unitOfWork.HealthPolicyRepository.AddRangeAsync(policies);

                var customer = _faker.GenerateCustomers(10);
                await _unitOfWork.CustomerRepository.AddRangeAsync(customer);
                await _unitOfWork.CommitAsync();

                var rules = _faker.GenerateRules(entitiesPolicy.ToList());
                await _unitOfWork.RuleRepository.AddRangeAsync(rules);

                _unitOfWork.CommitTransaction();
                
                await _unitOfWork.CommitAsync();
                return new SuccesResult();
            }
            catch (System.Exception)
            {
                _unitOfWork.RollbackTransaction();
                throw;
            }

        }
    }
}
