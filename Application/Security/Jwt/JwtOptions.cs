
namespace Application.Security.Jwt;

public class JwtOptions
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int DurationInMinutes { get; set; }
    public string Key { get; set; } 
}
