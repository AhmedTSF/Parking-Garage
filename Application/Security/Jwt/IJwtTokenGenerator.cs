using Domain.Entities;

namespace Application.Security.Jwt;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
