

using Application.DTOs.Customer;

namespace Application.DTOs.Car;

public class CreateCarDto
{
    public int CustomerId { get; set; }
    public string PlateNumber { get; set; }
    public CreateCustomerDto? CreateCustomer { get; set; }
}
