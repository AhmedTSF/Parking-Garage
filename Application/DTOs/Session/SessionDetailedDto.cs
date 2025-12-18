
namespace Application.DTOs.Session;

public class SessionDetailedDto
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string SpotNumber { get; set; }
    public string CarPlateNumber { get; set; }
    public string CustomerNationalId { get; set; }
    public string CustomerName { get; set; }
    public decimal CostPerHour { get; set; }
    public string? FinalCost { get; set; }
}
