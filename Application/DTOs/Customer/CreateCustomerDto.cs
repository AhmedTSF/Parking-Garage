using Application.DTOs.Car;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Customer;

public class CreateCustomerDto
{
    [Required]
    public string NationalId { get; set; } = null!;

    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;
}
