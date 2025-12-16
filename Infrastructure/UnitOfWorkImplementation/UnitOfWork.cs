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
    private IRepository<Session>? _sessions;
    private IRepository<Car>? _cars;
    private IRepository<Payment>? _payments;
    private ICustomerRepository? _customers;
    private ISpotRepository? _spots;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    // Lazy properties
    public IRepository<Session> Sessions => _sessions ??= new Repository<Session>(_context);
    public IRepository<Car> Cars => _cars ??= new Repository<Car>(_context);
    public IRepository<Payment> Payments => _payments ??= new Repository<Payment>(_context);
    public ICustomerRepository Customers => _customers ??= new CustomerRepository(_context);
    public ISpotRepository Spots => _spots ??= new SpotRepository(_context);

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
