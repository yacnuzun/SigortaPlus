using InsuranceApp.Core;
using InsuranceApp.Core.Entities;
using InsuranceApp.Core.EntityFramework;
using InsuranceApp.DataAccess.Persistance.DbConnection;

namespace InsuranceApp.DataAccess.Persistance.Repositories
{
    public class EfHealthPolicyDal : EfRepository<HealthPolicy, InsuranceAppDbContext>, IEfHealthPolicyDal
    {
        public EfHealthPolicyDal(InsuranceAppDbContext context) : base(context)
        {
        }
    }
}
