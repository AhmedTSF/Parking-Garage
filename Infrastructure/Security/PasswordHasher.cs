using Application.Security;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Security;

public class PasswordHasher : IHasher
{
    private const string Salt = "STATIC_SALT"; // for simplicity

    public string Hash(string password)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password + Salt));
        return Convert.ToBase64String(bytes);
    }

    public bool Verification(string inputPassword, string hashedPassword)
    {
        return Hash(inputPassword) == hashedPassword;
    }
}
