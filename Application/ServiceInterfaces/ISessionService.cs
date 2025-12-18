using Application.DTOs.Session;
using Domain.Common;

namespace Application.ServiceInterfaces;

public interface ISessionService
{
    Task<Result<int>> StartSessionAsync(CreateSessionDto dto);
    Task<Result<SessionDetailedDto>> EndSessionAsync(int sessionId);
    Task<Result<SessionDetailedDto>> GetByIdAsync(int sessionId);
    Task<List<SessionDetailedDto>> GetAllActiveAsync(int pageNumber, int pageSize);
    Task<List<SessionDetailedDto>> GetAllAsync(int pageNumber, int pageSize);
}
