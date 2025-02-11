using Asklepios.Application.Abstractions;
using Asklepios.Application.Queries.Users;
using Asklepios.Core.DTO.Users;
using Asklepios.Core.Repositories.Users;
using Asklepios.Infrastructure.DAL.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Asklepios.Infrastructure.DAL.Handlers;

internal sealed class GetAccountInfoHandler : IQueryHandler<GetAccountInfo, AccountDto>
{
    private readonly AsklepiosDbContext _dbContext;
    private readonly IAccountCacheRepository _cache;

    public GetAccountInfoHandler(AsklepiosDbContext dbContext, IAccountCacheRepository cache)
    {
        _dbContext = dbContext;
        _cache = cache;
    }

    public async Task<AccountDto> HandlerAsync(GetAccountInfo query)
    {
        var userId = query.UserId;
        var cachedUser = await _cache.GetAccountAsync(userId);
        if (cachedUser != null)
            return cachedUser;
        
        var user = await _dbContext.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.UserId == userId);

        var accountDto = user.AsAccountDto();
        await _cache.SetAccountAsync(userId, accountDto);

        return accountDto;
    }
}