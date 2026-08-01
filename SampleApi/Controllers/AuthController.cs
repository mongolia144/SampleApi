namespace SampleApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using SampleApi.DTOs.Auth;
using SampleApi.Interfaces.AuthInterfaces;
using SampleApi.Models;
using SampleApi.Results;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }



    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDTO loginDTO)
    {
        var serviceResult = await _authService.Login(loginDTO);

        if (!serviceResult.Success)
        {
            _logger.LogWarning("AuthController.Login Failed: Reason={Reason}", ServiceResult<AuthResponseDTO>.ErrorsToString(serviceResult.Errors));
            return BadRequest(serviceResult.Errors);
        }

        _logger.LogInformation("AuthController.Login User Logged In: {Email}", loginDTO.Email);
        return Ok(serviceResult.Data);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(LoginDTOAdd loginDTOAdd)
    {
        var serviceResult = await _authService.Register(loginDTOAdd);

        if (!serviceResult.Success)
        {
            _logger.LogWarning("AuthController.Register Failed: Reason={Reason}", ServiceResult<AuthResponseDTO>.ErrorsToString(serviceResult.Errors));
            return BadRequest(serviceResult.Errors);
        }
        
        
        _logger.LogInformation("AuthController.Register UserRegistered: {Email}", serviceResult.Data?.Email);

        return Ok(serviceResult.Data);


    }
}
