using Application.DTOs.Spot;
using Domain.Common;

namespace Application.ServiceInterfaces;

public interface ISpotService
{
    Task<Result<SpotDto>> GetByIdAsync(int id); 
    Task<int> CreateAsync(CreateSpotDto dto);
    Task<IEnumerable<SpotDto>> GetAllAsync(int pageNumber, int pageSize);
    Task<IEnumerable<AvailableSpotDto>> GetAvailableSpotsAsync(int pageNumber, int pageSize);
    Task<int> GetAvailableSpotCountAsync();
}
