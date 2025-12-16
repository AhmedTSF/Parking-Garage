using Domain.ValueObjects;

namespace Domain.Entities;

public class Session
{
    public int Id { get; set; }
    public DateTimeSlot DateTimeSlot { get; set; } 
    public decimal CostPerHour { get; set; }
    public decimal? FinalCost => DateTimeSlot.TotalHours.HasValue
    ? CostPerHour * DateTimeSlot.TotalHours.Value
    : null;

    public int CarId { get; set; }
    public int SpotId { get; set; }
    public Car Car { get; set; }
    public Spot Spot { get; set; }
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
