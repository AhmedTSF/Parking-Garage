
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.RepositoryImplementations;

public class PaymentRepository : Repository<Payment>, IPaymentRepository
{
    public PaymentRepository(AppDbContext context) : base(context) { }

    public async Task<Payment?> GetBySessionIdAsync(int sessionId)
    {
        return await _context.Payments.FirstOrDefaultAsync(p => p.SessionId == sessionId);
    }

    public async Task<Payment?> GetDetailedBySessionIdAsync(int sessionId)
    {
        return await _context.Payments
                .Include(p => p.Session)
                    .ThenInclude(s => s.Car)
                        .ThenInclude(c => c.Customer)
                .Include(p => p.Session.Spot)
                .FirstOrDefaultAsync(p => p.SessionId == sessionId);
    }

    public async Task<List<Payment>> GetAllDetailedAsync(int pageNumber, int pageSize)
    {
        return await _context.Payments
                .Include(p => p.Session)
                    .ThenInclude(s => s.Car)
                        .ThenInclude(c => c.Customer)
                .Include(p => p.Session.Spot)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
    }


}
