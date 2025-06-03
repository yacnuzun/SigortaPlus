using InsuranceApp.Core.Interfaces;

namespace InsuranceApp.Core.Entities
{
    public class OfferHealthPolicy : BaseEntity, IEntity
    {
        public int OfferId { get; set; }
        public Offer Offer { get; set; }

        public int HealthPolicyId { get; set; }
        public HealthPolicy HealthPolicy { get; set; }
    }


}
