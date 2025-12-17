using Domain.Common;

namespace Domain.Entities;

public class Customer
{
    public const string InvalidDataError = "Invalid customer information provided.";


    public int Id { get; set;} 
    public string NationalId { get; set;} 
    public string FirstName { get; set;} 
    public string LastName { get; set;} 
    public ICollection<Car> Cars { get; set; } = new List<Car>();

    public string FullName()
    {
        return $"{FirstName} {LastName}";
    }


    public static Result<Customer> TryCreate( 
        string nationalId,
        string firstName,
        string lastName)
    {
        if (string.IsNullOrWhiteSpace(nationalId))
            return Result<Customer>.Failure(InvalidDataError);

        if (string.IsNullOrWhiteSpace(firstName))
            return Result<Customer>.Failure(InvalidDataError);

        if (string.IsNullOrWhiteSpace(lastName))
            return Result<Customer>.Failure(InvalidDataError);

        return Result<Customer>.Success(new Customer
        {
            NationalId = nationalId,
            FirstName = firstName,
            LastName = lastName
        });
    }


}
