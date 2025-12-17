using Domain.Entities;
using Domain.RepositoryInterfaces;

namespace Domain.UnitOfWorksInterfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<Payment> Payments { get; }
    ISessionRepository Sessions { get; }
    ICarRepository Cars { get; }
    ICustomerRepository Customers { get; }
    ISpotRepository Spots { get; } 
    ISittingRepository Sittings { get; }
    Task CommitAsync();
} 
