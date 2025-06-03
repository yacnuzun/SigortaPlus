using InsuranceApp.Core.Interfaces;

namespace InsuranceApp.Entities
{
    public class Rule : BaseEntity, IEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        // FK
        public int OfferId { get; set; }
        public virtual Offer Offer { get; set; }
    }

}
