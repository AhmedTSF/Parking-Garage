
using Domain.Common;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.RepositoryImplementations;

public class SessionRepository : Repository<Session>, ISessionRepository
{
    public SessionRepository(AppDbContext context) : base(context) { }


    public async Task<Session?> GetAcitveDetailedByIdAsync(int id)
    {
        return await _context.Sessions
            .AsNoTracking()
            .Include(s => s.Car)
            .ThenInclude(c => c.Customer)
            .Include(s => s.Spot)
            .Where(s => s.Id == id && s.DateTimeSlot.ExitTimestamp == null)
            .FirstOrDefaultAsync(); 

    }

    public async Task<Session?> GetDetailedByIdAsync(int id)
    {
        return await _context.Sessions
            .AsNoTracking()
            .Include(s => s.Car)
            .ThenInclude(c => c.Customer)
            .Include(s => s.Spot)
            .Include(s => s.Payment)
            .Where(s => s.Id == id)
            .FirstOrDefaultAsync();

    }

    public async Task<List<Session>> GetAllActiveDetailedAsync(int pageNumber, int pageSize)
    {
        return await _context.Sessions
            .AsNoTracking()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(s => s.Car)
            .ThenInclude(c => c.Customer)
            .Include(s => s.Spot)
            .ToListAsync();
    }

    public async Task<List<Session>> GetAllDetailedAsync(int pageNumber, int pageSize)
    {
        return await _context.Sessions
            .AsNoTracking()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(s => s.Car)
            .ThenInclude(c => c.Customer)
            .Include(s => s.Spot)
            .ToListAsync();
    }



}
