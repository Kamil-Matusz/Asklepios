using Asklepios.Core.DTO.Users;

namespace Asklepios.Application.Services.Users;

public interface IUserService
{
    Task<List<UserAutocompleteDto>> GetAutocompleteAsync(string search, int limit = 10);
    Task<List<UserAutocompleteDto>> GetNursesList();
}