using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SampleApi.DTOs.Auth;
using SampleApi.DTOs.Movies;
using SampleApi.Interfaces.AuthInterfaces;
using SampleApi.Interfaces.MovieInterfaces;
using SampleApi.Interfaces.UserInterfaces;
using SampleApi.Mappings.MovieMapping;
using SampleApi.Models;
using SampleApi.Results;
using SampleApi.Services.AuthServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SampleApi.Services.AuthServices;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IConfiguration _config;
    private readonly IValidator<User> _userValidator;

    public AuthService(IUserRepository userRepository,  IConfiguration config, IPasswordHasher passwordHasher, IValidator<User> userValidator)
    {
        _userRepository = userRepository;
        _config = config;
        _passwordHasher = passwordHasher;
        _userValidator = userValidator;
    }

    public async Task<ServiceResult<AuthResponseDTO>> Login(LoginDTO loginDTO)
    {
        var user = await _userRepository.GetByEmail(loginDTO.Email);

        if (user == null)
            return ServiceResult<AuthResponseDTO>.Fail(["Invalid email or password"]);

        var passwordHash = this._passwordHasher.HashPassword(loginDTO.Password, user.Salt);

        if (user.PasswordHash != passwordHash)
            return ServiceResult<AuthResponseDTO>.Fail(["Invalid email or password"]);

        var token = this.GenerateJwtToken(user);

        var response = new AuthResponseDTO
        {
            Token = token,
            Email = user.Email
        };

        return ServiceResult<AuthResponseDTO>.Ok(response);
    }


    public async Task<ServiceResult<AuthResponseDTO>> Register(LoginDTOAdd loginDTOAdd)
    {
        var serviceResult = new ServiceResult<LoginDTO>();
        // map
        var userEntity = UserMapping.MapFromLoginDTOAddToUserEntity(loginDTOAdd);
        userEntity.Salt = GenerateSalt();
        userEntity.PasswordHash = _passwordHasher.HashPassword(loginDTOAdd.Password, userEntity.Salt);
        // validate
        var validationResult = _userValidator.Validate(userEntity);
        if (!validationResult.IsValid)
            return ServiceResult<AuthResponseDTO>.Fail(validationResult.Errors);
        //EF Core tracks the entity that we have just added
        await _userRepository.Add(userEntity);
        
        var token = this.GenerateJwtToken(userEntity);

        var response = new AuthResponseDTO
        {
            Token = token,
            Email = userEntity.Email
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

    public byte[] GenerateSalt(int size = 32)
    {
        var salt = new byte[size];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);
        return salt;
    }



}
