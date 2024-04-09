using Asklepios.Application.Abstractions;
using Asklepios.Application.Queries;
using Asklepios.Core.DTO.Users;
using Asklepios.Infrastructure.DAL.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Asklepios.Infrastructure.DAL.Handlers;

internal sealed class GetAccountInfoHandler : IQueryHandler<GetAccountInfo, AccountDto>
{
    private readonly AsklepiosDbContext _dbContext;

    public GetAccountInfoHandler(AsklepiosDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AccountDto> HandlerAsync(GetAccountInfo query)
    {
        var userId = query.UserId;
        var user = await _dbContext.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.UserId == userId);

        return user?.AsAccountDto();
    }
}