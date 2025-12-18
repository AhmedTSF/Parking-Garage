

using Application.DTOs.Customer;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Car;

public class CreateCarDto
{
    [Required]
    public string PlateNumber { get; set; } = null!;

    // Existing customer
    public int? CustomerId { get; set; }

    // New customer
    public CreateCustomerDto? Customer { get; set; }
}
