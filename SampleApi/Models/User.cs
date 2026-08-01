namespace SampleApi.Models;

public class User
{
    public string Id { get; set; } = Guid.NewGuid().ToString(); 
    public string Email { get; set; } = string.Empty;
    public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
    public byte[] Salt { get; set; } = Array.Empty<byte>();

}
