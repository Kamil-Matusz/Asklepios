using Asklepios.Core.Entities.Users;
using Asklepios.Core.ValueObjects;

namespace Asklepios.Core.Repositories.Users;

public interface IUserRepository
{
    Task<User> GetUserByIdAsync(Guid userId);
    Task<User> GetUserByEmailAsync(string email);
    Task AddUserAsync(User user);
    Task<bool> CheckAccountActivity(string email);
    Task DeleteUserAsync(User user);
    Task ChangeUserRoleAsync(Guid userId, Role role);
    Task ChangeAccountStatusAsync(Guid userId, bool status);
    Task ChangeUserPassword(Guid userId, string password);
    Task<List<User>> GetAutocompleteUsers(string search, int limit = 10);
    Task<List<User>> GetNursesList();
    Task<List<User>> GetDoctorsList();
}