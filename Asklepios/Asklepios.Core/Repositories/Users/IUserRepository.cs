using Asklepios.Core.Entities.Users;

namespace Asklepios.Infrastructure.Repositories.Users;

public interface IUserRepository
{
    Task<User> GetUserByIdAsync(Guid userId);
    Task<User> GetUserByEmailAsync(string email);
    Task AddUserAsync(User user);
}