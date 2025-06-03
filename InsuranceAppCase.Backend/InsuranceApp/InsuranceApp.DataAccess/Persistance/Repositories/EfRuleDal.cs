using InsuranceApp.Core;
using InsuranceApp.Core.Entities;
using InsuranceApp.DataAccess.Persistance.DbConnection;

namespace InsuranceApp.DataAccess.Persistance.Repositories
{
    public class EfRuleDal : EfRepository<Rule, InsuranceAppDbContext>, IRepository<Rule>
    {
        public EfRuleDal(InsuranceAppDbContext context) : base(context)
        {
        }
    }
}
