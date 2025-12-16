
namespace Domain.Entities;

public class Spot
{
    public int Id { get; set; } 
    public string SpotNumber { get; set; } 
    public bool IsOccupied { get; set; } 
    public ICollection<Session> Sessions { get; set; } = new List<Session>();
}
