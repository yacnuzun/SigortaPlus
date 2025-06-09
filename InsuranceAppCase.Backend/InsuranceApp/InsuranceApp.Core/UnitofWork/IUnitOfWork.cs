using InsuranceApp.Core.EntityFramework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace InsuranceApp.Core.UnitofWork
{
    public interface IUnitOfWork : IDisposable
    {
        IEfCustomerDal CustomerRepository { get; }
        IEfHealthPolicyDal HealthPolicyRepository { get; }
        IEfRuleDal RuleRepository { get; }
        IEfOfferDal OfferRepository { get; }

        Task<int> CommitAsync(CancellationToken cancellationToken = default);
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        void Rollback();
    }
}
