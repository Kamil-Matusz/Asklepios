using Asklepios.Application.Abstractions;
using Asklepios.Application.Commands.Users;
using Asklepios.Application.Security;
using Asklepios.Application.Services.Clock;
using Asklepios.Application.Services.Email;
using Asklepios.Core.Entities.Users;
using Asklepios.Core.Exceptions.Users;
using Asklepios.Core.Repositories.Users;
using Asklepios.Core.ValueObjects;

namespace Asklepios.Application.Commands.Handlers.Users;

internal sealed class SignUpToClinicHandler : ICommandHandler<SignUpToClinic>
{
    private readonly IClock _clock;
    private readonly IPasswordManager _passwordManager;
    private IUserRepository _userRepository;
    private readonly IEmailService _emailService;

    public SignUpToClinicHandler(IClock clock, IPasswordManager passwordManager, IUserRepository userRepository, IEmailService emailService)
    {
        _clock = clock;
        _passwordManager = passwordManager;
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task HandlerAsync(SignUpToClinic command)
    {
        var role = Role.Patient();
        bool accountStatus = true;
        
        if (await _userRepository.GetUserByEmailAsync(command.Email) is not null)
        {
            throw new EmailAlreadyInUseException(command.Email);
        }
        
        var securedPassword = _passwordManager.Secure(command.Password);
        var user = new User(command.UserId, command.Email, securedPassword, role, accountStatus, _clock.CurrentDate());
        
        await _userRepository.AddUserAsync(user);
        await _emailService.SendEmailWithHelloMessageAsync(command.Email);
    }
}