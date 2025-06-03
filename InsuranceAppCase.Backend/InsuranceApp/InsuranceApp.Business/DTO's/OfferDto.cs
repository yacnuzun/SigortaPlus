using System;
using System.Collections.Generic;

namespace InsuranceApp.Business.DTO_s
{
    public class OfferDto 
    {
        public string OfferCode { get; set; }
        public decimal Price { get; set; }
        public DateTime ValidUntil { get; set; }
        public List<OfferHealthPolicyDto> HealthPolicyDtos { get; set; }
    }
}
