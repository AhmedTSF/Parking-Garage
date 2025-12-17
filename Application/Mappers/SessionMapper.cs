
using Application.DTOs.Session;
using Domain.Entities;

namespace Application.Mappers;

public static class SessionMapper
{

    public static SessionDetailedDto ToDetailedDto(this Session entity)
    {
        return new SessionDetailedDto
        {
            Id = entity.Id,
            StartTime = entity.DateTimeSlot.EntryTimestamp,
            EndTime = entity.DateTimeSlot.ExitTimestamp!.Value,
            SpotNumber = entity.Spot.SpotNumber,
            CarPlateNumber = entity.Car.PlateNumber,
            CustomerNationalId = entity.Car.Customer.NationalId,
            CustomerName = entity.Car.Customer.FullName(),
            CostPerHour = entity.CostPerHour,
            FinalCost = entity.FinalCost!.Value
        }; 
    }

}
