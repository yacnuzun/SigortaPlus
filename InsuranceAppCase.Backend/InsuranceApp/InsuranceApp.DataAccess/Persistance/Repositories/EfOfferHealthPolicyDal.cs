using InsuranceApp.Core.Entities;
using InsuranceApp.Core.EntityFramework;
using InsuranceApp.DataAccess.Persistance.DbConnection;

namespace InsuranceApp.DataAccess.Persistance.Repositories
{
    public class EfOfferHealthPolicyDal : EfRepository<OfferHealthPolicy, InsuranceAppDbContext>, IEfOfferHealthPolicyDal
    {
        public EfOfferHealthPolicyDal(InsuranceAppDbContext context) : base(context)
        {
        }
    }
}
