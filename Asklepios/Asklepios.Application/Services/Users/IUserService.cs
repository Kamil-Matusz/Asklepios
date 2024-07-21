using Asklepios.Core.DTO.Users;

namespace Asklepios.Application.Services.Users;

public interface IUserService
{
    Task<List<UserDto>> GetAutocompleteAsync(string search, int limit = 10);
}