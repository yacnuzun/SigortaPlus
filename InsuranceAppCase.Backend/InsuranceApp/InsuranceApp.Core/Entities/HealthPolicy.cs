using InsuranceApp.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace InsuranceApp.Core.Entities
{
    public class HealthPolicy : BaseEntity, IEntity
    {
        public string PolicyNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual ICollection<Rule> Rules { get; set; }

        // Many-to-many
        public virtual ICollection<OfferHealthPolicy> OfferHealthPolicies { get; set; }
    }

}
