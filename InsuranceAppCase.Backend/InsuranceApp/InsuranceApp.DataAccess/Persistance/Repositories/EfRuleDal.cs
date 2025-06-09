using InsuranceApp.Core;
using InsuranceApp.Core.Entities;
using InsuranceApp.Core.EntityFramework;
using InsuranceApp.DataAccess.Persistance.DbConnection;

namespace InsuranceApp.DataAccess.Persistance.Repositories
{
    public class EfRuleDal : EfRepository<Rule, InsuranceAppDbContext>, IEfRuleDal
    {
        public EfRuleDal(InsuranceAppDbContext context) : base(context)
        {
        }
    }
}
