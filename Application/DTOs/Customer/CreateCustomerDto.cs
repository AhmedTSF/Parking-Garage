using Application.DTOs.Car;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Customer;

public class CreateCustomerDto
{
    [Required]
    public string NationalId { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string PhoneNumber { get; set; }
}
