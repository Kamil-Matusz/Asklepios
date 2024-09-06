using Asklepios.Application.Abstractions;
using Asklepios.Application.Commands.Users;
using Asklepios.Application.Security;
using Asklepios.Core.Repositories.Users;

namespace Asklepios.Application.Commands.Handlers.Users;

internal sealed class ChangeUserPasswordHandler : ICommandHandler<ChangeUserPassword>
{
    private readonly IPasswordManager _passwordManager;
    private readonly IUserRepository _userRepository;

    public ChangeUserPasswordHandler(IPasswordManager passwordManager, IUserRepository userRepository)
    {
        _passwordManager = passwordManager;
        _userRepository = userRepository;
    }

    public async Task HandlerAsync(ChangeUserPassword command)
    {
        var securedPassword = _passwordManager.Secure(command.Password);

        await _userRepository.ChangeUserPassword(command.UserId, securedPassword);
    }
}