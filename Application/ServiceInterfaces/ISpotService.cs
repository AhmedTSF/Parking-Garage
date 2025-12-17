using Application.DTOs.Spot;

namespace Application.ServiceInterfaces;

public interface ISpotService
{
    Task<int> CreateAsync(CreateSpotDto dto);
    Task<IEnumerable<SpotDto>> GetAllAsync(int pageNumber, int pageSize);
    Task<IEnumerable<AvailableSpotDto>> GetAvailableSpotsAsync(int pageNumber, int pageSize);
    Task<int> GetAvailableSpotCountAsync();
}
