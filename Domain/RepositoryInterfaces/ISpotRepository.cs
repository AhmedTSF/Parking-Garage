using Domain.Entities;

namespace Domain.RepositoryInterfaces;

public interface ISpotRepository : IRepository<Spot>
{
    Task<Spot> GetBySpotNubmerAsync(string spotNumber);
    Task<IEnumerable<Spot>> GetAvailableSpotsAsync(int pageNumber, int pageSize);
    Task<int> GetAvailableSpotCountAsync();
}
