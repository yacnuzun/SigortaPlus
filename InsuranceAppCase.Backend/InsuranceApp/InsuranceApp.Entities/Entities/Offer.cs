using InsuranceApp.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace InsuranceApp.Entities
{
    public class Offer : BaseEntity, IEntity
    {
        public string OfferCode { get; set; }
        public decimal Price { get; set; }
        public DateTime ValidUntil { get; set; }

        // FK
        public int HealthPolicyId { get; set; }
        public virtual HealthPolicy HealthPolicy { get; set; }

        public virtual ICollection<Rule> Rules { get; set; }
    }

}
