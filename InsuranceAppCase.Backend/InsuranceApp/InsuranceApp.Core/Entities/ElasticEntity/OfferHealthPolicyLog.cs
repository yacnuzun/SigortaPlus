using InsuranceApp.Core.Interfaces;

namespace InsuranceApp.Core.Entities
{
    public class OfferHealthPolicyLog : BaseElasticSearchEntity, IEntity
    {
        public int? OfferId { get; set; }
        public string Action { get; set; }
        public string Message { get; set; }
    }
}
