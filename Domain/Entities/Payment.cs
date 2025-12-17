
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


    public static Result<Payment> TryCreate(Session session)
    {
        if (session is null)
            return Result<Payment>.Failure("Session is required when creating a payment.");

        return Result<Payment>.Success(new Payment
        {
            SessionId = session.Id,
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