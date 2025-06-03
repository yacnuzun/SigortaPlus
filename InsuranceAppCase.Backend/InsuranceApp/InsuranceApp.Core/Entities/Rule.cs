using InsuranceApp.Core.Interfaces;

namespace InsuranceApp.Core.Entities
{
    public class Rule : BaseEntity, IEntity
    {

        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public int Gender { get; set; }

        public int HealthPolicyId { get; set; }
        public virtual HealthPolicy HealthPolicy { get; set; }


    }


}
