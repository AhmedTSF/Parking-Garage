using Application.DTOs.Customer;
using Domain.Common;
using Domain.Entities;
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
        Task<Result<int>> CreateAsync(CreateCustomerDto dto);
    }
}
