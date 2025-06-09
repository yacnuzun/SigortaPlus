using InsuranceApp.Core.Interfaces;

namespace InsuranceApp.Core.DTO_s
{
    public class CustomerResponseDto : IDto
    {
        public string FullName { get; set; }
        public int Age { get; set; }
        public int Gender { get; set; }
    }
}
