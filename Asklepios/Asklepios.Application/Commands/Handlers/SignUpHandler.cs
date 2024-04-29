using Asklepios.Application.Abstractions;
using Asklepios.Application.Security;
using Asklepios.Application.Services.Clock;
using Asklepios.Core.Entities.Users;
using Asklepios.Core.Exceptions.Users;
using Asklepios.Core.Repositories.Users;
using Asklepios.Core.ValueObjects;

namespace Asklepios.Application.Commands.Handlers;

internal sealed class SignUpHandler : ICommandHandler<SignUp>
{
    private readonly IClock _clock;
    private readonly IPasswordManager _passwordManager;
    private IUserRepository _userRepository;

    public SignUpHandler(IClock clock, IPasswordManager passwordManager, IUserRepository userRepository)
    {
        _clock = clock;
        _passwordManager = passwordManager;
        _userRepository = userRepository;
    }
    
    public async Task HandlerAsync(SignUp command)
    {
        var role = string.IsNullOrWhiteSpace(command.Role) ? Role.IT_Employee() : new Role(command.Role);
        
        if (await _userRepository.GetUserByEmailAsync(command.Email) is not null)
        {
            throw new EmailAlreadyInUseException(command.Email);
        }
        
        var securedPassword = _passwordManager.Secure(command.Password);
        var user = new User(command.UserId, command.Email, securedPassword, command.Role, command.IsActive, _clock.CurrentDate());

        await _userRepository.AddUserAsync(user);
    }
}