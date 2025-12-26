using Domain.Entities;
using Domain.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.RepositoryImplementations;

public class CarRepository : Repository<Car>, ICarRepository
{
    public CarRepository(AppDbContext context) : base(context) { }
    public async Task<IEnumerable<Car>> GetAllDetailedAsync(int pageNumber, int pageSize)
    {
        return await _context.Cars
            .Include(c => c.Customer)

            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public Task<Car?> GetByPlateNumberAsync(string plateNumber)
    {
        return _dbSet.FirstOrDefaultAsync(c => c.PlateNumber == plateNumber);
    }
}
