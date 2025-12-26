using Domain.Common;
using Domain.Entities.Abstracts;

namespace Domain.Entities;

public class Customer : Person
{
    public const string InvalidDataError = "Invalid customer information provided.";

    public ICollection<Car> Cars { get; set; } = new List<Car>();

    public static Result<Customer> TryCreate( 
        string nationalId,
        string firstName,
        string lastName,
        string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(nationalId))
            return Result<Customer>.Failure(InvalidDataError);

        if (string.IsNullOrWhiteSpace(firstName))
            return Result<Customer>.Failure(InvalidDataError);

        if (string.IsNullOrWhiteSpace(lastName))
            return Result<Customer>.Failure(InvalidDataError);

        if (string.IsNullOrWhiteSpace(phoneNumber))
            return Result<Customer>.Failure(InvalidDataError);

        return Result<Customer>.Success(new Customer
        {
            NationalId = nationalId,
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phoneNumber
        });
    }


}
