using Application.DTOs.Car;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Session;

public class CreateSessionDto
{
    // Existing car
    public string? PlateNumber { get; set; }

    // New car
    public CreateCarDto? CreateCar { get; set; }

    [Required(ErrorMessage = "SpotNumber is required.")]
    public string SpotNumber { get; set; } = null!;

} 
