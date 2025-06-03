using InsuranceApp.Business.DTO_s;
using InsuranceApp.Core.Helpers.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApp.Business.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IDataResult<CustomerResponseDto>> GetCustomer(int id);
        Task<IResult> Add(CustomerCreateDto customerCreateDto);
        Task<IDataResult<List<CustomerResponseDto>>> GetCustomers();
    }
}
