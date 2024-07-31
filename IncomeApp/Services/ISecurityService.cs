using IncomeApp.DTOs.Request;
using IncomeApp.DTOs.Response;
using Microsoft.AspNetCore.Identity.Data;

namespace IncomeApp.Services;

public interface ISecurityService
{
    Task RegisterUser(RegisterRequest model);
    Task<TokenDTO> LoginUser(LoginRequest loginRequest);
    Task<TokenDTO> RefreshToken(RefreshTokenRequestDTO refreshToken);
}