using InsuranceApp.Core.DTO_s;
using InsuranceApp.Core.Interfaces;

namespace InsuranceApp.Core.Entities
{
    public class CustomerElasticDto : BaseElasticSearchEntity, ISmartSearchConvertible
    {
        public string FullName { get; set; }
        public int Age { get; set; }
        public int Gender { get; set; }
        public static CustomerElasticDto GetViewModel(Customer customer)
        {
            return new CustomerElasticDto
            {
                Age = customer.Age,
                FullName = customer.FullName,
                Gender = customer.Gender,
                CreatedDate = customer.CreatedDate,
                Id = customer.Id,
                IsDeleted = customer.IsDeleted,
                UpdatedDate = customer.UpdatedDate,
                Description = customer.FullName,
                Title = customer.FullName
            };
        }

        public BaseElasticSearchEntity ToSmartSearchEntity()
        {
            return new BaseElasticSearchEntity
            {
                Id = this.Id,
                Title = this.FullName,
                Description = this.Description,
                Url = $"/customer/{this.Id}",
                Type = "Customer"
            };
        }

    }
}
