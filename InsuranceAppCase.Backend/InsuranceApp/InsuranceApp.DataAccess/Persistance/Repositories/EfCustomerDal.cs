using InsuranceApp.Core;
using InsuranceApp.Core.Entities;
using InsuranceApp.Core.EntityFramework;
using InsuranceApp.DataAccess.Persistance.DbConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApp.DataAccess.Persistance.Repositories
{
    public class EfCustomerDal : EfRepository<Customer, InsuranceAppDbContext>, IEfCustomerDal
    {
        public EfCustomerDal(InsuranceAppDbContext context) : base(context)
        {
        }
    }
}
