using Domain.Common;
using Domain.Entities;


namespace Domain.RepositoryInterfaces
{
    public interface ISessionRepository : IRepository<Session>
    {
        Task<Result<Session>> GetDetailedByIdAsync(int id);
        Task<Result<Session>> GetAcitveDetailedByIdAsync(int id);
        Task<List<Session>> GetAllDetailedAsync(int pageNumber, int pageSize);
        Task<List<Session>> GetAllActiveDetailedAsync(int pageNumber, int pageSize);
    }
}
