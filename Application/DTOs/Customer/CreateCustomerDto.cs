using Application.DTOs.Car;

namespace Application.DTOs.Customer;

public class CreateCustomerDto
{
    public string NationalId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public CreateCarDto Car { get; set; } = new();
}
