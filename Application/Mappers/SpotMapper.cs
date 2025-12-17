using Application.DTOs.Spot;
using Domain.Entities;

namespace Application.Mappers;

public static class SpotMapper
{
    public static Spot ToEntity(CreateSpotDto dto)
    {
        return new Spot
        {
            SpotNumber = dto.SpotNumber,
            IsOccupied = false
        };
    }

    public static SpotDto ToDto(Spot entity)
    {
        return new SpotDto
        {
            Id = entity.Id,
            SpotNumber = entity.SpotNumber,
            IsOccupied = entity.IsOccupied
        };
    }

    public static AvailableSpotDto ToAvailableDto(Spot entity)
    {
        return new AvailableSpotDto
        {
            Id = entity.Id,
            SpotNumber = entity.SpotNumber
        };
    }
}
