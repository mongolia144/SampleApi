using SampleApi.Models;
using SampleApi.DTOs.Auth;
using SampleApi.Results;

namespace SampleApi.Interfaces.AuthInterfaces;

public interface IAuthService
{
    Task<ServiceResult<AuthResponseDTO>> Login(LoginDTO loginDTO);
    Task<ServiceResult<AuthResponseDTO>> Register(LoginDTOAdd loginDTOAdd);
    string GenerateJwtToken(User user);

    byte[] GenerateSalt(int size = 32);
}
