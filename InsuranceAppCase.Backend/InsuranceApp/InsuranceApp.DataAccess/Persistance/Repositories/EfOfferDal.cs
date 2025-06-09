using InsuranceApp.Core.DTO_s;
using InsuranceApp.Core;
using InsuranceApp.Core.Entities;
using InsuranceApp.Core.EntityFramework;
using InsuranceApp.DataAccess.Persistance.DbConnection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceApp.DataAccess.Persistance.Repositories
{
    public class EfOfferDal : EfRepository<Offer, InsuranceAppDbContext>, IEfOfferDal
    {
        public EfOfferDal(InsuranceAppDbContext context) : base(context)
        {
        }

        public List<OfferResponseDto> GetMatchingHealthPolicies(OfferRequestDto request)
        {
            var result = (from rule in base.Context.Rules
                          where request.Age >= rule.MinAge &&
                                request.Age <= rule.MaxAge &&
                                (rule.Gender == 1) == request.Gender
                          join policy in base.Context.HealthPolicies
                              on rule.HealthPolicyId equals policy.Id
                          select new
                          {
                              PolicyNumber = policy.PolicyNumber,
                              StartDate = policy.StartDate,
                              EndDate = policy.EndDate
                          }).ToList(); // Artık veriler bellekte!





            return result.Select(policy => new OfferResponseDto
                          {
                              ResponseTitle = $"Health Policy #{policy.PolicyNumber}",
                              ResponseDescription = $"Valid from {policy.StartDate:yyyy-MM-dd} to {policy.EndDate:yyyy-MM-dd}",
                              PremiumPrice = 1000, // Buraya opsiyonel olarak bir hesaplama veya sabit değer gelebilir
                              ValidUntil = policy.EndDate
                          }).ToList();

            
        }

    }
}
