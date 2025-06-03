using InsuranceApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApp.DataAccess.Persistance.DbConnection
{
    public class InsuranceAppDbContext:DbContext
    {
        public InsuranceAppDbContext() : base("name=InsuranceAppDbContext")
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<HealthPolicy> HealthPolicies { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Rule> Rules { get; set; }
    }
}
