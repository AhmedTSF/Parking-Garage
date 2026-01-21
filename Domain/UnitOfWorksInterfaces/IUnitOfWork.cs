using Domain.Entities;
using Domain.RepositoryInterfaces;

namespace Domain.UnitOfWorksInterfaces;

public interface IUnitOfWork : IDisposable
{
    IPaymentRepository Payments { get; }
    ISessionRepository Sessions { get; }
    ICarRepository Cars { get; }
    ICustomerRepository Customers { get; }
    ISpotRepository Spots { get; } 
    IUserRepository Users { get; }
    ISittingRepository Settings { get; }
    
    Task CommitAsync();
} 
