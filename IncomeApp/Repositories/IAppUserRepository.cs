using IncomeApp.Models;

namespace IncomeApp.Repositories;

public interface IAppUserRepository
{
    Task AddUser(AppUser user);
    Task UpdateUser(AppUser user);
    Task<AppUser> GetUserByEmailAddress(string loginRequestEmail);
    Task<List<string>> GetUserRole(object id);
    Task<AppUser> GetUserByRefreshToken(string refreshTokenRefreshToken);
}