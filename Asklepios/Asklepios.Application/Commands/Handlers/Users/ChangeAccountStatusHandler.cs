using Asklepios.Application.Abstractions;
using Asklepios.Application.Commands.Users;
using Asklepios.Core.Repositories.Users;

namespace Asklepios.Application.Commands.Handlers.Users;

internal sealed class ChangeAccountStatusHandler : ICommandHandler<ChangeAccountStatus>
{
    private readonly IUserRepository _userRepository;

    public ChangeAccountStatusHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task HandlerAsync(ChangeAccountStatus command)
    {
        var userId = command.UserId;
        await _userRepository.ChangeAccountStatusAsync(userId, command.status);
    }
}