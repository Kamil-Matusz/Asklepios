using System.Security.Authentication;
using Asklepios.Application.Abstractions;
using Asklepios.Application.Security;
using Asklepios.Core.Exceptions.Users;
using Asklepios.Core.Repositories.Users;

namespace Asklepios.Application.Commands.Handlers;

internal sealed class SignInHandler : ICommandHandler<SignIn>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticator _authenticator;
    private readonly IPasswordManager _passwordManager;
    private readonly ITokenStorage _tokenStorage;

    public SignInHandler(IUserRepository userRepository, IAuthenticator authenticator, IPasswordManager passwordManager,ITokenStorage tokenStorage)
    {
        _userRepository = userRepository;
        _authenticator = authenticator;
        _passwordManager = passwordManager;
        _tokenStorage = tokenStorage;
    }
    
    public async Task HandlerAsync(SignIn command)
    {
        var user = await _userRepository.GetUserByEmailAsync(command.Email);
        if (user is null)
        {
            throw new InvalidCredentialException();
        }

        if (!_passwordManager.Validate(command.Password, user.Password))
        {
            throw new InvalidCredentialException();
        }

        bool accountIsActive = await _userRepository.CheckAccountActivity(command.Email);
        if (accountIsActive is false)
        {
            throw new AccountIsNotActiveException(command.Email);
        }

        var jwt = _authenticator.CreateToken(user.UserId, user.Role);
        _tokenStorage.SetToken(jwt);
    }
}