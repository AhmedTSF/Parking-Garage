
namespace Domain.Entities;

public class Car
{
    public int Id { get; set;} 
    public string PlateNumber { get; set;} 
    public int CustomerId { get; set; } 
    public Customer Customer { get; set; }
    public ICollection<Session> Sessions { get; set; } = new List<Session>();
}
