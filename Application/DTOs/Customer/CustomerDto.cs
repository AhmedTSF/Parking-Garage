using Application.DTOs.Car;


namespace Application.DTOs.Customer;

public class CustomerDto
{
    public int Id { get; set; }
    public string NationalId { get; set; }
    public string FullName { get; set; }
    public List<CarDto> Cars { get; set; } = new();
}
