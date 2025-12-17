
using Domain.Common;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.RepositoryImplementations;

public class SessionRepository : Repository<Session>, ISessionRepository
{
    public SessionRepository(AppDbContext context) : base(context) { }
    public async Task<Result<Session>> GetAcitveDetailedByIdAsync(int id)
    {
        var session = await _context.Sessions
            .AsNoTracking()
            .Include(s => s.Car)
            .ThenInclude(c => c.Customer)
            .Include(s => s.Spot)
            .Where(s => s.Id == id && s.DateTimeSlot.ExitTimestamp == null)
            .FirstOrDefaultAsync(); 

        if (session != null)
            return Result<Session>.Success(session);

        return Result<Session>.Failure($"Active session with ID {id} not found.");
    }

    public async Task<Result<Session>> GetDetailedByIdAsync(int id)
    {
        var session = await _context.Sessions
            .AsNoTracking()
            .Include(s => s.Car)
            .ThenInclude(c => c.Customer)
            .Include(s => s.Spot)
            .Include(s => s.Payments)
            .Where(s => s.Id == id)
            .FirstOrDefaultAsync();

        if (session != null)
            return Result<Session>.Success(session);

        return Result<Session>.Failure($"Session with ID {id} not found.");
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
