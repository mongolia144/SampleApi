namespace SampleApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using SampleApi.DTOs.Auth;
using SampleApi.Interfaces.AuthInterfaces;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDTO loginDTO)
    {
        var serviceResult = await _authService.Login(loginDTO);

        if (!serviceResult.Success)
            return BadRequest(serviceResult.Errors);

        return Ok(serviceResult.Data);
    }

}
