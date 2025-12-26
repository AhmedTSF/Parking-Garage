
using Domain.Common;
using Domain.Entities.Abstracts;

namespace Domain.Entities;

public class User : Person
{
    public const string InvalidDataError = "Invalid user information provided.";

    public string Username { get; set; }
    public string HashedPassword { get; set; }
    public string Role { get; set; }
    public ICollection<Session> Sessions { get; set; }


    public static Result<User> TryCreate(
        string nationalId,
        string firstName,
        string lastName,
        string phoneNumber,
        string username,
        string role, 
        string hashedPassword)
    {
        if (string.IsNullOrWhiteSpace(nationalId))
            return Result<User>.Failure(InvalidDataError);

        if (string.IsNullOrWhiteSpace(firstName))
            return Result<User>.Failure(InvalidDataError);

        if (string.IsNullOrWhiteSpace(lastName))
            return Result<User>.Failure(InvalidDataError);

        if (string.IsNullOrWhiteSpace(phoneNumber))
            return Result<User>.Failure(InvalidDataError);

        if (string.IsNullOrWhiteSpace(username))
            return Result<User>.Failure(InvalidDataError);

        if (string.IsNullOrWhiteSpace(role))
            return Result<User>.Failure(InvalidDataError);

        if (string.IsNullOrWhiteSpace(hashedPassword))
            return Result<User>.Failure(InvalidDataError);

        return Result<User>.Success(new User
        {
            NationalId = nationalId,
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phoneNumber,
            Username = username,
            Role = role,
            HashedPassword = hashedPassword
        });
    }

}
