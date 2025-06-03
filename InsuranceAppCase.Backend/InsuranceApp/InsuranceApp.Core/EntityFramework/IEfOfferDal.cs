using InsuranceApp.Core.Entities;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;

namespace InsuranceApp.Core.EntityFramework
{
    public interface IEfOfferDal : IRepository<Offer>
    {
    }
}
