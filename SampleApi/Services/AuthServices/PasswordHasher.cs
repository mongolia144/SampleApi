using System.Security.Cryptography;
using System.Text;
using SampleApi.Interfaces.AuthInterfaces;

namespace SampleApi.Services.AuthServices;

public class PasswordHasher: IPasswordHasher
{
    public byte[] HashPassword(string password, byte[] salt)
    {
        using var pbkdf2 = new Rfc2898DeriveBytes(
            password,
            salt,
            100_000, // iterations
            HashAlgorithmName.SHA256);

        return pbkdf2.GetBytes(32); // 256-bit hash
    }

}
