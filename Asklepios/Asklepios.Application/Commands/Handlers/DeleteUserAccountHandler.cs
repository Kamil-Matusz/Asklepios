using Asklepios.Application.Abstractions;
using Asklepios.Core.Exceptions.Users;
using Asklepios.Core.Policies.Users;
using Asklepios.Core.Repositories.Users;

namespace Asklepios.Application.Commands.Handlers;

internal sealed class DeleteUserAccountHandler : ICommandHandler<DeleteUserAccount>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserDeletionPolicy _userDeletionPolicy;

    public DeleteUserAccountHandler(IUserRepository userRepository, IUserDeletionPolicy userDeletionPolicy)
    {
        _userRepository = userRepository;
        _userDeletionPolicy = userDeletionPolicy;
    }

    public async Task HandlerAsync(DeleteUserAccount command)
    {
        var user = await _userRepository.GetUserByIdAsync(command.UserId);
        if (user is null)
        {
            throw new UserNotFoundException(command.UserId);
        }
        
        if (await _userDeletionPolicy.CannotDeleteUserPolicy(command.UserId) is false)
        {
            throw new CannotDeleteUserException(command.UserId);
        }

        await _userRepository.DeleteUserAsync(user);
    }
}