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
        var users = dbContext.Users.AsQueryable();
        if (!users.Any())
        {
            var newUsers = new List<User>()
            {
                new User(Guid.Parse("cfbc979f-2431-45f7-989f-28e4d60c0dc6"), "admin@asklepios.com", _passwordManager.Secure("password"), Role.Admin(), true, DateTime.UtcNow),
                new User(Guid.Parse("0a99194f-12ff-4e78-9ebf-9a6976d813ac"), "employee@asklepios.com", _passwordManager.Secure("password"), Role.IT_Employee(), true, DateTime.UtcNow),
                new User(Guid.Parse("91fc545f-4732-419f-812a-9e9603b515c6"), "miloszmichalski@asklepios.com", _passwordManager.Secure("password"), Role.Doctor(), true, DateTime.UtcNow),
                new User(Guid.Parse("c4377bd0-73f3-4487-be66-e22557b7c76e"), "kamilmatusz@asklepios.com", _passwordManager.Secure("password"), Role.Doctor(), true, DateTime.UtcNow),
                new User(Guid.Parse("02f29611-0909-4222-8614-02539baf5daf"), "joannakulig@asklepios.com", _passwordManager.Secure("password"), Role.Nurse(), true, DateTime.UtcNow),
                new User(Guid.Parse("c742606a-0763-4545-81bd-b72105f5cfcd"), "annapajak@asklepios.com", _passwordManager.Secure("password"), Role.Nurse(), true, DateTime.UtcNow),
                new User(Guid.Parse("6ce705e0-5c6e-400f-b321-6c961206c7cb"), "paulinalasek@asklepios.com", _passwordManager.Secure("password"), Role.Nurse(), true, DateTime.UtcNow),
                new User(Guid.Parse("56efc67d-9d7a-441d-88a4-4b5e63b27ef7"), "andrzejkowalski@asklepios.com", _passwordManager.Secure("password"), Role.Doctor(), true, DateTime.UtcNow),
                new User(Guid.Parse("a4c69f2b-dcf3-4e8b-baa6-1275122c4e68"), "martapiotrowska@asklepios.com", _passwordManager.Secure("password"), Role.Nurse(), true, DateTime.UtcNow),
                new User(Guid.Parse("dcfc4b58-3d1b-4b5e-888a-14d3c56f4785"), "lukasznawrocki@asklepios.com", _passwordManager.Secure("password"), Role.Patient(), true, DateTime.UtcNow),
                new User(Guid.Parse("bb4a0fcf-81b7-43b9-9cdd-d78cf89f6f2c"), "barbaranowak@asklepios.com", _passwordManager.Secure("password"), Role.Nurse(), true, DateTime.UtcNow),
                new User(Guid.Parse("ae9e2a5d-7aa4-47ad-bbdd-768c8bdf4a64"), "grzegorzadamczyk@asklepios.com", _passwordManager.Secure("password"), Role.Doctor(), true, DateTime.UtcNow),
            };
            
            dbContext.Users.AddRange(newUsers);
            await dbContext.SaveChangesAsync();
        }
    }
}