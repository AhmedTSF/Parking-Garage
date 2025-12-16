using Domain.Entities;
using Domain.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Infrastructure.RepositoryImplementations;

public class SpotRepository : Repository<Spot>, ISpotRepository
{
    public SpotRepository(AppDbContext context) : base(context) { }

    // Return all available spots
    public async Task<IEnumerable<Spot>> GetAvailableSpotsAsync(int pageNumber, int pageSize)
    {
        // Assuming Spot has a property like IsAvailable
        return await _dbSet.Where(s => !s.IsOccupied)
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();
    }

    // Return count of available spots
    public async Task<int> GetAvailableSpotCountAsync()
    {
        return await _dbSet.CountAsync(s => !s.IsOccupied);
    }
}
