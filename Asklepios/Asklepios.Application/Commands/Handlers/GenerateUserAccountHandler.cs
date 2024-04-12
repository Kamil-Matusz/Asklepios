using System.Security.Cryptography;
using Asklepios.Application.Abstractions;
using Asklepios.Application.Security;
using Asklepios.Application.Services.Clock;
using Asklepios.Core.Entities.Users;
using Asklepios.Core.Exceptions.Users;
using Asklepios.Core.Repositories.Users;
using Asklepios.Core.ValueObjects;

namespace Asklepios.Application.Commands.Handlers;

internal sealed class GenerateUserAccountHandler : ICommandHandler<GenerateUserAccount>
{
    private readonly IClock _clock;
    private readonly IPasswordManager _passwordManager;
    private IUserRepository _userRepository;

    public GenerateUserAccountHandler(IClock clock, IPasswordManager passwordManager, IUserRepository userRepository)
    {
        _clock = clock;
        _passwordManager = passwordManager;
        _userRepository = userRepository;
    }

    public async Task HandlerAsync(GenerateUserAccount command)
    {
        var role = string.IsNullOrWhiteSpace(command.Role) ? Role.No_Role() : new Role(command.Role);
        
        if (await _userRepository.GetUserByEmailAsync(command.Email) is not null)
        {
            throw new EmailAlreadyInUseException(command.Email);
        }

        var password = GeneratePassword();
        var securedPassword = _passwordManager.Secure(password);
        var user = new User(command.UserId, command.Email, securedPassword, command.Role, command.IsActive, _clock.CurrentDate());

        await _userRepository.AddUserAsync(user);
    }
    
    public static string GeneratePassword(int length = 8)
    {
        const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()-_";
        char[] chars = new char[length];
        using (var rng = new RNGCryptoServiceProvider())
        {
            byte[] data = new byte[length];
            rng.GetBytes(data);
            for (int i = 0; i < length; i++)
            {
                int index = data[i] % validChars.Length;
                chars[i] = validChars[index];
            }
        }
        return new string(chars);
    }
}