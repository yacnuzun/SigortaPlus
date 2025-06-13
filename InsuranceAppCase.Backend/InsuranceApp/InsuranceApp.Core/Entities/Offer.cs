using InsuranceApp.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace InsuranceApp.Core.Entities
{
    public class Offer : BaseEntity, IEntity
    {
        public string OfferCode { get; set; }
        public decimal Price { get; set; }
        public DateTime ValidUntil { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        // Many-to-many
        public virtual ICollection<OfferHealthPolicy> OfferHealthPolicies { get; set; }
    }
}
