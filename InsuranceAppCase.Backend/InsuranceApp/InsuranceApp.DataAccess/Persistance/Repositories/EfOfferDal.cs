using InsuranceApp.Core;
using InsuranceApp.Core.Entities;
using InsuranceApp.DataAccess.Persistance.DbConnection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceApp.DataAccess.Persistance.Repositories
{
    public class EfOfferDal : EfRepository<Offer, InsuranceAppDbContext>, IRepository<Offer>
    {
        public EfOfferDal(InsuranceAppDbContext context) : base(context)
        {
        }

        public async Task<List<Offer>> CreateanOffer(Customer customer,int age, int gender)
        {
            //var result = await (from rule in base.Context.Rules
            //                    join policy in base.Context.HealthPolicies
            //                    on rule.HealthPolicyId equals policy.Id
            //                    where age >= rule.MinAge && age <= rule.MaxAge
            //                          && (rule.Gender == 0 || rule.Gender == gender)
            //                    select new Offer
            //                    {
            //                         = policy.Id,
            //                        HealthPolicy = policy,
                                    
            //                        ResponseTitle = policy.Title,
            //                        ResponseDescription = policy.Description
            //                    }).ToListAsync();

            return null;
        }
    }
}
