using InsuranceApp.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace InsuranceApp.Entities
{
    public class HealthPolicy : BaseEntity, IEntity
    {
        public string PolicyNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Foreign Key
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        // Navigasyon
        public virtual ICollection<Offer> Offers { get; set; }
    }

}
