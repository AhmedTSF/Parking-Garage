using Domain.Entities;

namespace Domain.RepositoryInterfaces;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<Customer?> GetDetailedByNationalIdAsync(string nationalId);
    Task<Customer?> GetByNationalIdAsync(string nationalId);
}
