using Asklepios.Application.Security;
using Asklepios.Core.Entities.Users;
using Asklepios.Core.ValueObjects;
using Asklepios.Infrastructure.DAL.PostgreSQL;

namespace Asklepios.Infrastructure.DAL.Seeders.Users;

public class UsersSeeder : IOrderedSeeder
{
    private readonly IPasswordManager _passwordManager;

    public UsersSeeder(IPasswordManager passwordManager)
    {
        _passwordManager = passwordManager;
    }

    // first seeder
    public int Order => 1; 
    
    public async Task SeedAsync(AsklepiosDbContext dbContext)
    {
        var users = dbContext.Users.ToList();
        if (!users.Any())
        {
            var newUsers = new List<User>()
            {
                new User(Guid.Parse("cfbc979f-2431-45f7-989f-28e4d60c0dc6"), "admin@asklepios.com", _passwordManager.Secure("password"), Role.Admin(), true, DateTime.UtcNow),
                new User(Guid.Parse("0a99194f-12ff-4e78-9ebf-9a6976d813ac"), "employee@asklepios.com", _passwordManager.Secure("password"), Role.IT_Employee(), true, DateTime.UtcNow),
                new User(Guid.Parse("c4377bd0-73f3-4487-be66-e22557b7c76e"), "kamilmatusz@asklepios.com", _passwordManager.Secure("password"), Role.Doctor(), true, DateTime.UtcNow),
                new User(Guid.Parse("02f29611-0909-4222-8614-02539baf5daf"), "joannakulig@asklepios.com", _passwordManager.Secure("password"), Role.Nurse(), true, DateTime.UtcNow)
            };
            
            dbContext.Users.AddRange(newUsers);
            await dbContext.SaveChangesAsync();
        }
    }
}