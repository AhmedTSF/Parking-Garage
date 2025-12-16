using Domain.Entities;

namespace Domain.RepositoryInterfaces;

public interface ISpotRepository : IRepository<Spot>
{
    Task<IEnumerable<Spot>> GetAvailableSpotsAsync(int pageNumber, int pageSize);
    Task<int> GetAvailableSpotCountAsync();
}
