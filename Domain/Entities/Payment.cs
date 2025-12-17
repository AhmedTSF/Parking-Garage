
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Payment
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaidAt { get; set; } 
    public PaymentMethod PaymentMethod { get; set; }
    public int SessionId { get; set; }
    public Session Session { get; set; }


    public static Result<Payment> TryCreate(int sessionId)
    {
        if (sessionId <= 0)
            return Result<Payment>.Failure("Invalid session ID.");

        return Result<Payment>.Success(new Payment
        {
            SessionId = sessionId,
            Amount = 0,
            PaymentMethod = PaymentMethod.NotDetected,
            PaidAt = DateTime.MinValue
        });
    }

    public Result<bool> TryPay(decimal amount, PaymentMethod method) 
    {
        if (PaymentMethod != PaymentMethod.NotDetected)
            return Result<bool>.Failure("Payment is already completed.");

        if (amount <= 0)
            return Result<bool>.Failure("Payment amount must be greater than zero.");

        PaymentMethod = method;
        Amount = amount;
        PaidAt = DateTime.Now;

        return Result<bool>.Success(true);
    }
}