namespace SampleApi.DTOs.Auth;

//This is what the client receives after a successful login
public class AuthResponseDTO
{
    public string Token { get; set; } = string.Empty;
    public string Email { get; set; }=string.Empty;
}