using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Session
{
    public const string InvalidDataError = "Invalid session information provided.";

    public int Id { get; set; }
    public DateTimeSlot DateTimeSlot { get; set; } 
    public decimal CostPerHour { get; set; }
    public decimal? FinalCost => DateTimeSlot.TotalHours.HasValue
    ? CostPerHour * DateTimeSlot.TotalHours.Value
    : null;

    public int CarId { get; set; }
    public int SpotId { get; set; }
    public int CreatedUserId { get; set; }
    public Car Car { get; set; }
    public Spot Spot { get; set; }
    public Payment Payment { get; set; }
    public User CreatedUser { get; set; }
    public static Result<Session> TryCreate( 
        DateTime entryTimestamp, 
        int spotId,
        Car car,
        decimal costPerHour)
    {
        if (entryTimestamp == default)
            return Result<Session>.Failure(InvalidDataError);

        if (car is null)
            return Result<Session>.Failure(InvalidDataError);

        if(spotId <= 0)
            return Result<Session>.Failure(InvalidDataError);

        if(costPerHour <= 0)
            return Result<Session>.Failure(InvalidDataError);

        return Result<Session>.Success(new Session
        {
            DateTimeSlot = new DateTimeSlot(entryTimestamp),
            SpotId = spotId,
            Car = car,
            CostPerHour = costPerHour
        });
    }

    public Result EndSession()
    {
        if (this.Id <= 0)
            return Result.Failure("Session must be created before ending it.");

        if(DateTimeSlot.ExitTimestamp.HasValue)
            return Result.Failure("Session has already been ended.");

        DateTimeSlot.EndSession(DateTime.Now);

        return Result.Success();
    }

}
