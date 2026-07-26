using SampleApi.Models;
using SampleApi.Interfaces.AuthInterfaces;
using SampleApi.Interfaces.UserInterfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SampleApi.DTOs.Auth;
using SampleApi.Results;
using Microsoft.AspNetCore.Identity;
using SampleApi.Services.AuthServices;

namespace SampleApi.Services.AuthServices;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IConfiguration _config;

    public AuthService(IUserRepository userRepository,  IConfiguration config, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _config = config;
        _passwordHasher = passwordHasher;
    }

    public async Task<ServiceResult<AuthResponseDTO>> Login(LoginDTO loginDTO)
{
    var user = await _userRepository.GetByEmail(loginDTO.Email);

    if (user == null)
        return ServiceResult<AuthResponseDTO>.Fail(["Invalid email or password"]);


    var hashedPassword = this._passwordHasher.Hash(loginDTO.Password, user.Salt);

    if (user.HashedPassword != hashedPassword)
        return ServiceResult<AuthResponseDTO>.Fail(["Invalid email or password"]);

    var token = this.GenerateJwtToken(user);

    var response = new AuthResponseDTO
    {
        Token = token,
        Email = user.Email
    };

    return ServiceResult<AuthResponseDTO>.Ok(response);
}


    public string GenerateJwtToken(User user)
    {
        var jwtSection = _config.GetSection("Jwt");

        var keyString = jwtSection.GetValue<string>("Key");
        if (keyString is null)
            throw new Exception("JWT Key is missing in configuration");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email)
        };

        var token = new JwtSecurityToken(
            issuer: jwtSection["Issuer"],
            audience: jwtSection["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(jwtSection.GetValue<int>("ExpiresInMinutes")),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
