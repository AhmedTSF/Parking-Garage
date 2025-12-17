using Application.DTOs.Car;

namespace Application.DTOs.Session;

public class CreateSessionDto
{
    public string? PlateNumber { get; set; } 
    public CreateCarDto? CreateCar{ get; set; }
    public string? SpotNumber { get; set; } 
} 
