
using Domain.Entities;

namespace Domain.RepositoryInterfaces;

public interface ICarRepository : IRepository<Car>
{
    Task<Car?> GetByPlateNumberAsync(string plateNumber);
    Task<IEnumerable<Car>> GetAllDetailedAsync(int pageNumber, int pageSize); 
}