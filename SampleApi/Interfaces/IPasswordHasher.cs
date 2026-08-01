namespace SampleApi.Interfaces.AuthInterfaces;

public interface IPasswordHasher
{
    byte[] HashPassword(string password, byte[] salt);
}