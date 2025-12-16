namespace Domain.Entities;

public class Customer
{
    public int Id { get; set;} // PK [cite: 49]
    public string NationalId { get; set;} // [cite: 49]
    public string FirstName { get; set;} // [cite: 48]
    public string LastName { get; set;} // [cite: 47]

    // Navigation property for 1:M relationship with Car
    public ICollection<Car> Cars { get; set; } = new List<Car>();
}
