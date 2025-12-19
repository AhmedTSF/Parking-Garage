using Domain.Enums;

namespace Application.DTOs.Payment;

public class PaymentDetailedDto
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaidAt { get; set; }
    public string PaymentMethod { get; set; }

    public int SessionId { get; set; }
    public DateTime EntryTimestamp { get; set; }
    public DateTime? ExitTimestamp { get; set; }
    public decimal CostPerHour { get; set; }
    public decimal? FinalCost { get; set; }

    public string CarPlateNumber { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string SpotNumber { get; set; }
}
