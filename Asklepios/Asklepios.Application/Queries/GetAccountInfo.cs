using Asklepios.Application.Abstractions;
using Asklepios.Core.DTO.Users;

namespace Asklepios.Application.Queries;

public class GetAccountInfo : IQuery<AccountDto>
{
    public Guid UserId { get; set; }
}