using Asklepios.Application.Abstractions;
using Asklepios.Application.Commands.Users;
using Asklepios.Core.Exceptions.Users;
using Asklepios.Core.Repositories.Users;
using Asklepios.Core.ValueObjects;

namespace Asklepios.Application.Commands.Handlers.Users;

internal sealed class ChangeUserRoleHandler : ICommandHandler<ChangeUserRole>
{
    private readonly IUserRepository _userRepository;

    public ChangeUserRoleHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task HandlerAsync(ChangeUserRole command)
    {
        var userId = command.UserId;
        var role = string.IsNullOrWhiteSpace(command.Role) ? Role.No_Role() : new Role(command.Role);
        if (role == "Admin" || role == "Doctor" || role == "Nurse" || role == "IT Employee" || role == "No Role")
        {
            await _userRepository.ChangeUserRoleAsync(userId, role);    
        }
        else
        {
            throw new UserRoleNotExistException();
        }
    }
}