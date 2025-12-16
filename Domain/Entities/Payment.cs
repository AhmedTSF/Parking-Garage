
namespace Domain.Entities;

public class Payment
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime Timestamp { get; set; }
    public int SessionId { get; set; }
    public Session Session { get; set; }
}