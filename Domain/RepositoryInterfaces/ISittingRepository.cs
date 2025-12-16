using Domain.Settings;

namespace Domain.RepositoryInterfaces;

public interface ISittingRepository
{
    Task<Sitting> GetAsync(string key);
    Task<string?> GetValueAsync(string key);
    Task SetAsync(string key, string value);
    Task SaveChangesAsync();
}
