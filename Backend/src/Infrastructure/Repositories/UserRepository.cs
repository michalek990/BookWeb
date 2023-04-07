using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Infrastructure.Presistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context) { }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await Context.Users
            .Where(u => u.Username == username)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> ExistByUsername(string username)
    {
        return await Context.Users
            .AnyAsync(u => u.Username == username);
    }
    
    public async Task<bool> IsUserBanned(string username)
    {
        return await Context.Users
            .Where(u => u.Username == username)
            .AnyAsync(u => u.AccountStatus == AccountStatus.Banned);
    }
}