using InsuranceApp.Core.Entities;
using System.Collections.Generic;

namespace InsuranceApp.Core.Helpers.Faker
{
    public interface IFakerService
    {
        List<HealthPolicy> GenerateHealthPolicies(int count);
        List<Customer> GenerateCustomers(int count);
        List<Entities.Rule> GenerateRules(List<HealthPolicy> policies, int rulePerPolicy = 2);
    }
}
