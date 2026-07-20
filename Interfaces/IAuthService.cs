using SampleApi.Models;
using SampleApi.DTOs.Auth;
using SampleApi.Results;

namespace SampleApi.Interfaces.AuthInterfaces;

public interface IAuthService
{
    Task<ServiceResult<AuthResponseDTO>> Login(LoginDTO loginDTO);
    string GenerateJwtToken(User user);
}
