
using Domain.Entities.Abstracts;

namespace Domain.Entities;

public class User : Person
{
    public string Username { get; set; }
    public string HashedPassword { get; set; }
    public string Role { get; set; }
    public ICollection<Session> Sessions { get; set; }
}
