using Asklepios.Core.DTO.Users;

namespace Asklepios.Application.Security;

public interface ITokenStorage
{
    void SetToken(JwtDto jwt);
    JwtDto GetToken();
}