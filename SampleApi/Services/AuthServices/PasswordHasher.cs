using System.Security.Cryptography;
using System.Text;
using SampleApi.Interfaces.AuthInterfaces;

namespace SampleApi.Services.AuthServices;

public class PasswordHasher: IPasswordHasher
{
    public string Hash(string password, string salt)
    {
        using var sha = SHA256.Create();
        var combined = Encoding.UTF8.GetBytes(password + salt);
        var hash = sha.ComputeHash(combined);
        return Convert.ToHexString(hash).ToLower();
    }
}
