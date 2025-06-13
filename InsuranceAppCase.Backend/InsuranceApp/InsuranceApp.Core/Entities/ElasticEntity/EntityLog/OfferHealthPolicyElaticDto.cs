using InsuranceApp.Core.Interfaces;

namespace InsuranceApp.Core.Entities
{
    public class OfferHealthPolicyElaticDto : BaseElasticSearchEntity, IEntity
    {
        public int OfferId { get; set; }
        public OfferElasticDto Offer { get; set; }

        public int HealthPolicyId { get; set; }
        public HealthPolicyElasticDto HealthPolicy { get; set; }
    }
}
