using Domain.Entities;
using Domain.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;


namespace Infrastructure.RepositoryImplementations;

public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    public CustomerRepository(AppDbContext context) : base(context){ }

    public async Task<Customer> GetByNationalIdAsync(string nationalId)
    {
        // Assuming NationalId is unique
        return await _dbSet.FirstOrDefaultAsync(c => c.NationalId == nationalId);
    }
}
