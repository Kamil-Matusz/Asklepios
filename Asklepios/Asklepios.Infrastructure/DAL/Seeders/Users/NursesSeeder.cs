using Asklepios.Core.Entities.Users;
using Asklepios.Infrastructure.DAL.PostgreSQL;

namespace Asklepios.Infrastructure.DAL.Seeders.Users;

public class NursesSeeder : IOrderedSeeder
{
    public int Order => 4;
    public async Task SeedAsync(AsklepiosDbContext dbContext)
    {
        var nurses = dbContext.Nurses.AsQueryable();
        if (!nurses.Any())
        {
            var newNurses = new List<Nurse>()
            {
                new Nurse(Guid.Parse("5f3704a4-60f5-4f51-84ae-74d99547e96e"), "Joanna", "Kulig", Guid.Parse("02f29611-0909-4222-8614-02539baf5daf"), Guid.Parse("2a9f1b22-1854-43f2-a30b-c34d3d70f2c9")),
                new Nurse(Guid.Parse("0fe3d736-792f-40b1-847e-3f9172a71fba"), "Anna", "Pająk", Guid.Parse("c742606a-0763-4545-81bd-b72105f5cfcd"), Guid.Parse("2a9f1b22-1854-43f2-a30b-c34d3d70f2c9")),
                new Nurse(Guid.Parse("602a9ee4-544b-4df0-a15b-660c55384994"), "Jolanta", "Risso", Guid.Parse("6ce705e0-5c6e-400f-b321-6c961206c7cb"), Guid.Parse("2a9f1b22-1854-43f2-a30b-c34d3d70f2c9")),
                new Nurse(Guid.Parse("602a9ff4-544b-4df0-a15b-660c55384994"), "Barbara", "Nowak", Guid.Parse("bb4a0fcf-81b7-43b9-9cdd-d78cf89f6f2c"), Guid.Parse("2a9f1b22-1854-43f2-a30b-c34d3d70f2c9")),
                new Nurse(Guid.Parse("602a9ff4-544b-4df0-a14a-660c55384994"), "Marta", "Piotrowska", Guid.Parse("a4c69f2b-dcf3-4e8b-baa6-1275122c4e68"), Guid.Parse("2a9f1b22-1854-43f2-a30b-c34d3d70f2c9"))
            };
            
            dbContext.Nurses.AddRange(newNurses);
            await dbContext.SaveChangesAsync();
        }
    }
}