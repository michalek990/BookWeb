using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByUsernameAsync(string username);
    Task<bool> ExistByUsername(string username);
    Task<bool> IsUserBanned(string username);
}