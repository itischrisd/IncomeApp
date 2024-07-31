using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using IncomeApp.DTOs.Request;
using IncomeApp.DTOs.Response;
using IncomeApp.Models;
using IncomeApp.Repositories;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;

namespace IncomeApp.Services;

public class SecurityService(IAppUserRepository appUserRepository, IConfiguration configuration) : ISecurityService
{
    public async Task RegisterUser(RegisterRequest model)
    {
        var hashedPasswordAndSalt = GetHashedPasswordAndSalt(model.Password);

        var user = new AppUser
        {
            Email = model.Email,
            Password = hashedPasswordAndSalt.Item1,
            Salt = hashedPasswordAndSalt.Item2,
            RefreshToken = GenerateRefreshToken(),
            RefreshTokenExp = DateTime.Now.AddDays(1)
        };

        await appUserRepository.AddUser(user);
    }

    public async Task<TokenDTO> LoginUser(LoginRequest loginRequest)
    {
        var user = await appUserRepository.GetUserByEmailAddress(loginRequest.Email);

        var passwordHashFromDb = user.Password;
        var curHashedPassword = GetHashedPasswordWithSalt(loginRequest.Password, user.Salt);

        if (passwordHashFromDb != curHashedPassword) throw new UnauthorizedAccessException();

        var useRoles = await appUserRepository.GetUserRole(user.IdUser);
        var userclaim = new List<Claim>();
        userclaim.AddRange(useRoles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecretKey"]!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            "https://localhost:7153",
            "https://localhost:7153",
            userclaim,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds
        );

        user.RefreshToken = GenerateRefreshToken();
        user.RefreshTokenExp = DateTime.Now.AddDays(1);

        await appUserRepository.UpdateUser(user);

        return new TokenDTO
            { AccessToken = new JwtSecurityTokenHandler().WriteToken(token), RefreshToken = user.RefreshToken };
    }

    public async Task<TokenDTO> RefreshToken(RefreshTokenRequestDTO refreshToken)
    {
        var user = await appUserRepository.GetUserByRefreshToken(refreshToken.RefreshToken);
        if (user == null) throw new SecurityTokenException("Invalid refresh token");

        if (user.RefreshTokenExp < DateTime.Now) throw new SecurityTokenException("Refresh token expired");

        var useRoles = await appUserRepository.GetUserRole(user.IdUser);
        var userclaim = new List<Claim>();
        userclaim.AddRange(useRoles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecretKey"]!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var jwtToken = new JwtSecurityToken(
            "https://localhost:7153",
            "https://localhost:7153",
            userclaim,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds
        );

        user.RefreshToken = GenerateRefreshToken();
        user.RefreshTokenExp = DateTime.Now.AddDays(1);

        await appUserRepository.UpdateUser(user);

        return new TokenDTO
            { AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken), RefreshToken = user.RefreshToken };
    }

    private static string GetHashedPasswordWithSalt(string loginRequestPassword, string userSalt)
    {
        var saltBytes = Convert.FromBase64String(userSalt);

        var currentHashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            loginRequestPassword,
            saltBytes,
            KeyDerivationPrf.HMACSHA1,
            10000,
            256 / 8));

        return currentHashedPassword;
    }

    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private static Tuple<string, string> GetHashedPasswordAndSalt(string modelPassword)
    {
        var salt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            modelPassword,
            salt,
            KeyDerivationPrf.HMACSHA1,
            10000,
            256 / 8));

        var saltBase64 = Convert.ToBase64String(salt);

        return new Tuple<string, string>(hashed, saltBase64);
    }
}