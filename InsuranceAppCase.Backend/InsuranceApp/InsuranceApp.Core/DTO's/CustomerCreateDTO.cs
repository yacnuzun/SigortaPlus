using System.Collections.Generic;

namespace InsuranceApp.Core.DTO_s
{
    public class CustomerCreateDto
    {
        public string FullName { get; set; }
        public int Age { get; set; }
        public int Gender { get; set; }
        public List<OfferDto> Offers { get; set; }
    }
}
