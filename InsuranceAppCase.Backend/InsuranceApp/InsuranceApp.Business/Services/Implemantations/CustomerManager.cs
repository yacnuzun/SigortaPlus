using InsuranceApp.Business.DTO_s;
using InsuranceApp.Core.Entities;
using InsuranceApp.Business.Services.Interfaces;
using InsuranceApp.Core.EntityFramework;
using InsuranceApp.Core.Helpers.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApp.Business
{
    public class CustomerManager : ICustomerService
    {
        private readonly IEfCustomerDal _efCustomerDal;

        public CustomerManager(IEfCustomerDal efCustomerDal)
        {
            _efCustomerDal = efCustomerDal;
        }

        public Task<IResult> Add(CustomerCreateDto customerCreateDto)
        {
            
            throw new NotImplementedException();
        }

        public async Task<IDataResult<CustomerResponseDto>> GetCustomer(int id)
        {
            var result = await _efCustomerDal.GetAsync(c => c.Id == id);
            if (result is null)
            {
                return new ErrorDataResult<CustomerResponseDto>(
                    new CustomerResponseDto
                    {
                        Age = result.Age,
                        FullName = result.FullName,
                        Gender = result.Gender,
                    });
            }

            return new SuccessDataResult<CustomerResponseDto>(
                new CustomerResponseDto
                {
                    Age = result.Age,
                    Gender = result.Gender,
                    FullName = result.FullName,
                }
                );
        }

        public async Task<IDataResult<List<CustomerResponseDto>>> GetCustomers()
        {
            try
            {
                IEnumerable<Customer> result = await _efCustomerDal.ListAsync();
                List<CustomerResponseDto> dtoResult = result.Select(c => new CustomerResponseDto
                {
                    Age = c.Age,
                    FullName = c.FullName,
                    Gender = c.Gender
                }).ToList();
                return new SuccessDataResult<List<CustomerResponseDto>>(dtoResult);
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
