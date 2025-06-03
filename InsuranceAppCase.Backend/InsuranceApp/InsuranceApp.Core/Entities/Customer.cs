using InsuranceApp.Core.Interfaces;
using System.Collections.Generic;

namespace InsuranceApp.Core.Entities
{
    public class Customer : BaseEntity, IEntity
    {
        public string FullName { get; set; }
        public int Age { get; set; }
        public int Gender { get; set; }

        public virtual ICollection<Offer> Offers { get; set; }
    }


}
