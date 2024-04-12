using Asklepios.Core.Entities.Users;
using Asklepios.Core.Repositories.Users;

namespace Asklepios.Core.Policies.Users;

public class UserDeletionPolicy : IUserDeletionPolicy
{
    private readonly IUserRepository _userRepository;

    public UserDeletionPolicy(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> CannotDeleteUserPolicy(Guid userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);

        if (user.IsActive is false)
        {
            return true;
        }

        return false;
    }
}