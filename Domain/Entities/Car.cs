
using Domain.Common;

namespace Domain.Entities;

public class Car
{
    public const string InvalidDataError = "Invalid car information provided.";

    public int Id { get; set;} 
    public string PlateNumber { get; set;} 
    public int CustomerId { get; set; } 
    public Customer Customer { get; set; }
    public ICollection<Session> Sessions { get; set; } = new List<Session>();


    public static Result<Car> TryCreate(
        string plateNumber, 
        Customer customer)
    {
        if (string.IsNullOrWhiteSpace(plateNumber))
            return Result<Car>.Failure(InvalidDataError);

        if (customer is null)
            return Result<Car>.Failure(InvalidDataError);

        return Result<Car>.Success(new Car
        {
            PlateNumber = plateNumber,
            Customer = customer
        });
    }

}
