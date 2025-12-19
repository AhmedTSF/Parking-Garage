using Domain.Enums;

namespace Application.DTOs.Payment;

public class PaymentDto
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaidAt { get; set; }
    public string PaymentMethod { get; set; }
    public int SessionId { get; set; }
}
