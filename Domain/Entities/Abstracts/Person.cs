namespace Domain.Entities.Abstracts;

public abstract class Person
{
    public int Id { get; set; }
    public string NationalId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }

    public string FullName()
    {
        return $"{FirstName} {LastName}";
    }
}
