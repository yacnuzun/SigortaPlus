using InsuranceApp.Core;
using InsuranceApp.Core.Entities;
using InsuranceApp.DataAccess.Persistance.DbConnection;

namespace InsuranceApp.DataAccess.Persistance.Repositories
{
    public class EfHealthPolicyDal : EfRepository<HealthPolicy, InsuranceAppDbContext>, IRepository<HealthPolicy>
    {
        public EfHealthPolicyDal(InsuranceAppDbContext context) : base(context)
        {
        }
    }
}
