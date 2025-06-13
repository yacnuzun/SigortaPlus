using InsuranceApp.Core.Interfaces;
using System;

namespace InsuranceApp.Core.Entities
{
    public class HealthPolicyElasticDto: BaseElasticSearchEntity, IEntity
    {
        public string PolicyNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
