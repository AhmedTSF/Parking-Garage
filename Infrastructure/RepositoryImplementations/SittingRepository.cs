using Domain.RepositoryInterfaces;
using Domain.Settings;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.RepositoryImplementations;

public class SittingRepository : ISittingRepository
{
    private readonly AppDbContext _context;

    public SittingRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Sitting> GetAsync(string key)
    {
        return await _context.Set<Sitting>().FirstOrDefaultAsync(s => s.Key == key)
               ?? throw new KeyNotFoundException($"Setting with key '{key}' not found.");
    }

    public async Task<string?> GetValueAsync(string key)
    {
        var sitting = await _context.Set<Sitting>().FirstOrDefaultAsync(s => s.Key == key);
        return sitting?.Value;
    }

    public async Task SetAsync(string key, string value)
    {
        var sitting = await _context.Set<Sitting>().FirstOrDefaultAsync(s => s.Key == key);

        if (sitting == null)
        {
            // Add new setting
            await _context.Set<Sitting>().AddAsync(new Sitting (key, value));
        }
        else
        {
            // Update existing
            sitting.Value = value;
            _context.Set<Sitting>().Update(sitting);
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
