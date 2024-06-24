using Asklepios.Core.Repositories.Users;

namespace Asklepios.Core.Policies.Users;

public class RolePolicy : IRolePolicy
{
    private readonly IUserRepository _userRepository;

    public RolePolicy(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    public async Task<bool> CannotCreateNurse(Guid userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);

        if (user.Role == "Nurse")
        {
            return true;
        }

        return false;
    }

    public async Task<bool> CannotCreateDoctor(Guid userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);

        if (user.Role == "Doctor")
        {
            return true;
        }

        return false;
    }
}