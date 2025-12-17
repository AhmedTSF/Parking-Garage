using Application.DTOs.Spot;
using Application.Mappers;
using Application.ServiceInterfaces;
using Domain.UnitOfWorksInterfaces;

namespace Application.ServiceImplementations;

public class SpotService : ISpotService
{
    private readonly IUnitOfWork _unitOfWork;

    public SpotService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> CreateAsync(CreateSpotDto dto)
    {
        var spot = SpotMapper.ToEntity(dto);
        await _unitOfWork.Spots.AddAsync(spot);
        await _unitOfWork.CommitAsync();
        return spot.Id;
    }

    public async Task<IEnumerable<SpotDto>> GetAllAsync(int pageNumber, int pageSize)
    {
        var spots = await _unitOfWork.Spots.GetAllAsync(pageNumber, pageSize);
        return spots.Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(SpotMapper.ToDto);
    }

    public async Task<IEnumerable<AvailableSpotDto>> GetAvailableSpotsAsync(int pageNumber, int pageSize)
    {
        var spots = await _unitOfWork.Spots.GetAvailableSpotsAsync(pageNumber, pageSize);
        return spots.Select(SpotMapper.ToAvailableDto);
    }

    public async Task<int> GetAvailableSpotCountAsync()
    {
        return await _unitOfWork.Spots.GetAvailableSpotCountAsync();
    }
}
