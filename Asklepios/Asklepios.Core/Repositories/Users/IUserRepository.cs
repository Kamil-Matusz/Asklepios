using Asklepios.Core.Entities.Users;

namespace Asklepios.Core.Repositories.Users;

public interface IUserRepository
{
    Task<User> GetUserByIdAsync(Guid userId);
    Task<User> GetUserByEmailAsync(string email);
    Task AddUserAsync(User user);
    Task<bool> CheckAccountActivity(string email);
    Task DeleteUserAsync(User user);
}