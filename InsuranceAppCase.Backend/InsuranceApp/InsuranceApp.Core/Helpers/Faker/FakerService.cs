using Bogus;
using InsuranceApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace InsuranceApp.Core.Helpers.Faker
{
    public class FakerService : IFakerService
    {
        public List<Customer> GenerateCustomers(int count)
        {
            return new Faker<Customer>()
            .RuleFor(c => c.FullName, f => f.Name.FullName())
            .RuleFor(c => c.Age, f => f.Random.Int(18, 65))
            .RuleFor(c => c.Gender, f => f.PickRandom(0, 1)) // 0:Male, 1:Female gibi
            .Generate(count);
        }

        public List<HealthPolicy> GenerateHealthPolicies(int count)
        {
            return new Faker<HealthPolicy>()
                .RuleFor(h => h.PolicyNumber, f => f.Random.AlphaNumeric(10))
                .RuleFor(p => p.StartDate, f => f.Date.Past())
                .RuleFor(p => p.EndDate, (f, p) => p.StartDate.AddYears(1))
                .Generate(count);
        }
        public List<Entities.Rule> GenerateRules(List<HealthPolicy> policies, int rulePerPolicy = 2)
        {
            var faker = new Faker<Entities.Rule>()
                .RuleFor(r => r.MinAge, f => f.Random.Int(18, 30))
                .RuleFor(r => r.MaxAge, (f, r) => f.Random.Int(r.MinAge + 1, 60))
                .RuleFor(r => r.Gender, f => f.PickRandom(0, 1));

            var rules = new List<Entities.Rule>();

            foreach (var policy in policies)
            {
                var generatedRules = faker.Generate(2);
                foreach (var rule in generatedRules)
                {
                    rule.HealthPolicyId = policy.Id; // sonradan ata
                }
                rules.AddRange(generatedRules);
            }

            return rules;
        }

    }
}
