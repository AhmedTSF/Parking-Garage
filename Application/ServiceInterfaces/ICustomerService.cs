using Application.DTOs.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ServiceInterfaces
{
    public interface ICustomerService
    {
        Task<CustomerDto> GetByIdAsync(int id);
        Task<IEnumerable<CustomerDto>> GetAllAsync(int pageNumber, int pageSize);
        Task<int> CreateAsync(CreateCustomerDto dto);
    }
}
