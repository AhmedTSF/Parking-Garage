using Domain.Entities;
using Domain.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.RepositoryImplementations;

public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    public CustomerRepository(AppDbContext context) : base(context){ }

    public async Task<Customer?> GetByNationalIdAsync(string nationalId)
    {
        return await _dbSet.FirstOrDefaultAsync(c => c.NationalId == nationalId);
    }

    public Task<Customer?> GetDetailedByNationalIdAsync(string nationalId)
    {
        return _dbSet
            .Include(c => c.Cars)
            .FirstOrDefaultAsync(c => c.NationalId == nationalId);
    }
}
