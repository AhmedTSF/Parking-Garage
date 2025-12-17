using Domain.Entities;
using Domain.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.RepositoryImplementations;

public class SpotRepository : Repository<Spot>, ISpotRepository
{
    public SpotRepository(AppDbContext context) : base(context) { }

    public async Task<Spot?> GetBySpotNubmerAsync(string spotNumber)
    {
        return await _dbSet.FirstOrDefaultAsync(s => s.SpotNumber == spotNumber);
    }

    public async Task<IEnumerable<Spot>> GetAvailableSpotsAsync(int pageNumber, int pageSize)
    {
        // Assuming Spot has a property like IsAvailable
        return await _dbSet.Where(s => !s.IsOccupied)
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();
    }

    public async Task<int> GetAvailableSpotCountAsync()
    {
        return await _dbSet.CountAsync(s => !s.IsOccupied);
    }


}
