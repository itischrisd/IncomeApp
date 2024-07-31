using IncomeApp.Models;
using Microsoft.EntityFrameworkCore;

namespace IncomeApp.Repositories;

public class AppUserRepository(Context context) : IAppUserRepository
{
    public async Task AddUser(AppUser user)
    {
        await context.AppUsers.AddAsync(user);
        await context.SaveChangesAsync();
    }

    public async Task UpdateUser(AppUser user)
    {
        context.AppUsers.Update(user);
        await context.SaveChangesAsync();
    }

    public async Task<AppUser> GetUserByEmailAddress(string loginRequestEmail)
    {
        return (await context.AppUsers.FirstOrDefaultAsync(u => u.Email == loginRequestEmail))!;
    }

    public async Task<List<string>> GetUserRole(object id)
    {
        return (await context.AppUsers.FirstOrDefaultAsync(u => u.IdUser == (int)id))!.Roles;
    }

    public async Task<AppUser> GetUserByRefreshToken(string refreshTokenRefreshToken)
    {
        return (await context.AppUsers.FirstOrDefaultAsync(u => u.RefreshToken == refreshTokenRefreshToken))!;
    }
}