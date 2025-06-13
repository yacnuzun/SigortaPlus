using InsuranceApp.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace InsuranceApp.Core.DTO_s
{
    public class OfferResponseDto : IDto
    {
        public string ResponseTitle { get; set; }
        public string ResponseDescription { get; set; }
        public decimal PremiumPrice { get; set; }
        public DateTime ValidUntil { get; set; }
    }
    public class OfferResponseForeignKey
    {
        public IEnumerable<int> HealthKeys { get; set; }
        public int? CustomerId { get; set; }
    }
}
