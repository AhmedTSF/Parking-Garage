using Domain.Entities;
using Domain.RepositoryInterfaces;

namespace Domain.UnitOfWorksInterfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<Session> Sessions { get; }
    IRepository<Car> Cars { get; }
    IRepository<Payment> Payments { get; }
    ICustomerRepository Customers { get; }
    ISpotRepository Spots { get; } 
    Task CommitAsync();
} 
