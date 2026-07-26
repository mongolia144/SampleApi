namespace SampleApi.Interfaces.AuthInterfaces;

public interface IPasswordHasher
{
    string Hash(string password, string salt);
}