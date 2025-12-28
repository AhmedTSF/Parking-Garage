namespace Application.Security;

public interface IHasher
{
    // I didn't use (Salt) here, just for simplicity
    public string Hash(string password); 
    public bool Verification(string inputPassword, string hashedPassword);
}
