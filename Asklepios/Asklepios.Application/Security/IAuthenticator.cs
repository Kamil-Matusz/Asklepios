using Asklepios.Core.DTO.Users;

namespace Asklepios.Application.Security;

public interface IAuthenticator
{
    JwtDto CreateToken(Guid userId, string role);
}