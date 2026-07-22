namespace SampleApi.Models;

public class User
{
    public string? Id { get; set; }   // nullable fixes EF Core seeding
    public string Email { get; set; } = string.Empty;
    public string HashedPassword { get; set; } = string.Empty;
    public string Salt { get; set; } = string.Empty;
}
