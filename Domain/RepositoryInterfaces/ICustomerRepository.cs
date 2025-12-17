using Domain.Entities;

namespace Domain.RepositoryInterfaces;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<Customer?> GetByNationalIdAsync(string nationalId);
}
