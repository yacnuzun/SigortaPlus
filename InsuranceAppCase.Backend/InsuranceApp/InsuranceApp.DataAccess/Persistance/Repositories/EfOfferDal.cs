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

        public OfferResponseForeignKey GetMatchingHealthPolicies(Customer customer)
        {
            var result = (from rule in base.Context.Rules
                          where customer.Age >= rule.MinAge &&
                                customer.Age <= rule.MaxAge &&
                                rule.Gender  == customer.Gender &&
                                rule.IsDeleted == false
                          join policy in base.Context.HealthPolicies
                              on rule.HealthPolicyId equals policy.Id
                          where policy.IsDeleted == false
                          select new
                          {
                              PolicyId = policy.Id,
                          }).ToList(); // Artık veriler bellekte!





            return new OfferResponseForeignKey
            {
                CustomerId = customer.Id,
                HealthKeys = result.Select(p=>p.PolicyId).ToList()
            };

            
        }

    }
}
