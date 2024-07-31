using IncomeApp.DTOs.Request;
using IncomeApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace IncomeApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController(ISecurityService securityService) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterStudent(RegisterRequest model)
    {
        await securityService.RegisterUser(model);
        return Ok();
    }

    [Authorize]
    [HttpGet("test_auth")]
    public IActionResult GetAuthData()
    {
        return Ok("Secret data");
    }

    [AllowAnonymous]
    [HttpGet("test_noauth")]
    public IActionResult GetAnonData()
    {
        return Ok("Public data");
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        return Ok(await securityService.LoginUser(loginRequest));
    }

    [Authorize(AuthenticationSchemes = "IgnoreTokenExpirationScheme")]
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenRequestDTO refreshToken)
    {
        return Ok(await securityService.RefreshToken(refreshToken));
    }
}