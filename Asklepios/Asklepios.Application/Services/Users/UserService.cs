using Asklepios.Core.DTO.Users;
using Asklepios.Core.Entities.Users;
using Asklepios.Core.Repositories.Users;

namespace Asklepios.Application.Services.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<UserDto>> GetAutocompleteAsync(string search, int limit = 10)
    {
        var users = await _userRepository.GetAutocompleteUsers(search, limit);
        return users.Select(Map<UserDto>).ToList();
    }
    
    private static T Map<T>(User user) where T : UserDto, new() => new T()
    {
        UserId = user.UserId,
        Email = user.Email,
        Role = user.Role,
    };
}