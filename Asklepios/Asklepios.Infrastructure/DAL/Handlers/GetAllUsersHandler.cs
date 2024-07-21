using Asklepios.Application.Abstractions;
using Asklepios.Application.Queries.Users;
using Asklepios.Core.DTO.Users;
using Asklepios.Infrastructure.DAL.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Asklepios.Infrastructure.DAL.Handlers;

internal sealed class GetAllUsersHandler : IQueryHandler<GetAllUsers, IEnumerable<UserDto>>
{
    private readonly AsklepiosDbContext _dbContext;

    public GetAllUsersHandler(AsklepiosDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<UserDto>> HandlerAsync(GetAllUsers query)
    {
        var users = await _dbContext.Users
            .AsNoTracking()
            .OrderBy(x => x.Email)
                .Skip((query.PageIndex - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();
        
        return users.Select(user => user.AsUsersDto()).ToList();
    }
}