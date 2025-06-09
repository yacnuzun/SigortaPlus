using InsuranceApp.Core.EntityFramework;
using InsuranceApp.Core.UnitofWork;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InsuranceApp.DataAccess.Persistance.UnitofWork
{
    public class EfUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        private readonly TContext _context;
        private DbContextTransaction _transaction;

        public EfUnitOfWork(TContext context, 
            IEfCustomerDal customerRepository, 
            IEfHealthPolicyDal healthPolicyRepository, 
            IEfRuleDal ruleRepository, 
            IEfOfferDal offerRepository)
        {
            _context = context;
            CustomerRepository = customerRepository;
            HealthPolicyRepository = healthPolicyRepository;
            RuleRepository = ruleRepository;
            OfferRepository = offerRepository;
        }
        public IEfCustomerDal CustomerRepository { get; }

        public IEfHealthPolicyDal HealthPolicyRepository { get; }

        public IEfRuleDal RuleRepository { get; }

        public IEfOfferDal OfferRepository { get; }
        public void BeginTransaction()
        {
            if (_transaction == null)
                _transaction = _context.Database.BeginTransaction();
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void CommitTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Commit();
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public void RollbackTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public void Rollback()
        {
            var entries = _context.ChangeTracker.Entries()
                .Where(e => e.State != EntityState.Unchanged)
                .ToList();

            foreach (var entry in entries)
                entry.State = EntityState.Unchanged;
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}
