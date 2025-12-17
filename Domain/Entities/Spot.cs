
using Domain.Common;

namespace Domain.Entities;

public class Spot
{
    public int Id { get; set; } 
    public string SpotNumber { get; set; } 
    public bool IsOccupied { get; set; } 
    public ICollection<Session> Sessions { get; set; } = new List<Session>();


    public Result Occupy()
    {
        if (IsOccupied)
            return Result.Failure("Spot already occupied");

        IsOccupied = true;
        return Result.Success();
    }

    public Result Vacate()
    {
        if (!IsOccupied)
            return Result.Failure("Spot is not occupied");

        IsOccupied = false;
        return Result.Success();
    }



}
