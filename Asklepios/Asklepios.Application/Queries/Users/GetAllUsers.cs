using Asklepios.Application.Abstractions;
using Asklepios.Core.DTO.Users;

namespace Asklepios.Application.Queries.Users;

public class GetAllUsers : IQuery<IEnumerable<UserDto>>
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}