using Domain.Enums;

namespace Application.DTOs.Payment;

public class PayPaymentDto
{
    public int SessionId { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod Method { get; set; }
}
