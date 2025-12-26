using Domain.Entities;

namespace Domain.RepositoryInterfaces;

public interface IUserRepository : IRepository<User>
{
    public Task<User?> GetByNationalIdAsync(string nationalId);
}
