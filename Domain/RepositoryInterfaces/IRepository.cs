
namespace Domain.RepositoryInterfaces;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync(int pageNumber, int pageSize);
    Task AddAsync(T entity);
    void Update(T entity);
    Task DeleteAsync(int id);
    Task SaveChangesAsync();
}
