using Domain.Common;
using Domain.Entities;

namespace Domain.RepositoryInterfaces;

public interface IPaymentRepository : IRepository<Payment>
{
    Task<Payment?> GetBySessionIdAsync(int sessionId);
    Task<Payment?> GetDetailedBySessionIdAsync(int sessionId);
    Task<List<Payment>> GetAllDetailedAsync(int pageNumber, int pageSize);
    Task<List<Payment>> GetByCarPlateNumberAsync(string plateNumber);
}
