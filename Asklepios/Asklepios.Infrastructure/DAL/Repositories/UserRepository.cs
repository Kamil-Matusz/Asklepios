using Asklepios.Core.Entities.Departments;
using Asklepios.Core.Entities.Users;
using Asklepios.Infrastructure.DAL.PostgreSQL;
using Asklepios.Infrastructure.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace Asklepios.Infrastructure.DAL.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private readonly AsklepiosDbContext _dbContext;
    private readonly DbSet<User> _users;

    public UserRepository(AsklepiosDbContext dbContext)
    {
        _dbContext = dbContext;
        _users = _dbContext.Users;
    }

    public Task<User> GetUserByIdAsync(Guid userId) => _users.SingleOrDefaultAsync(x => x.UserId == userId);

    public Task<User> GetUserByEmailAsync(string email) => _users.SingleOrDefaultAsync(x => x.Email == email);

    public async Task AddUserAsync(User user)
    {
        await _users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }
}