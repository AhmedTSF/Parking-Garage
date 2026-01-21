using Domain.Entities;
using Domain.RepositoryInterfaces;
using Domain.UnitOfWorksInterfaces;
using Infrastructure.Data;
using Infrastructure.RepositoryImplementations;


namespace Infrastructure.UnitOfWorkImplementation;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    // Lazy fields for repositories
    private IPaymentRepository? _payments;
    private ISessionRepository? _sessions;
    private ICarRepository? _cars;
    private ICustomerRepository? _customers;
    private ISpotRepository? _spots;
    private IUserRepository? _users;
    private ISittingRepository? _sittings; 

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    // Lazy properties
    public IPaymentRepository Payments => _payments ??= new PaymentRepository(_context);
    public ISessionRepository Sessions => _sessions ??= new SessionRepository(_context);
    public ICarRepository Cars => _cars ??= new CarRepository(_context);
    public ICustomerRepository Customers => _customers ??= new CustomerRepository(_context);
    public ISpotRepository Spots => _spots ??= new SpotRepository(_context);
    public IUserRepository Users => _users ??= new UserRepository(_context);
    public ISittingRepository Settings => _sittings ??= new SittingRepository(_context);

    // Commit changes externally
    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
