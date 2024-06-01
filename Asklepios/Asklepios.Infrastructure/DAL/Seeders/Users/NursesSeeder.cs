using Asklepios.Core.Entities.Users;
using Asklepios.Infrastructure.DAL.PostgreSQL;

namespace Asklepios.Infrastructure.DAL.Seeders.Users;

public class NursesSeeder : IOrderedSeeder
{
    public int Order => 4;
    public async Task SeedAsync(AsklepiosDbContext dbContext)
    {
        var nurses = dbContext.Nurses.ToList();
        if (!nurses.Any())
        {
            var newNurses = new List<Nurse>()
            {
                new Nurse(Guid.Parse("5f3704a4-60f5-4f51-84ae-74d99547e96e"), "Joanna", "Kulig", Guid.Parse("02f29611-0909-4222-8614-02539baf5daf"), Guid.Parse("2a9f1b22-1854-43f2-a30b-c34d3d70f2c9")),
                new Nurse(Guid.Parse("0fe3d736-792f-40b1-847e-3f9172a71fba"), "Anna", "Pająk", Guid.Parse("c742606a-0763-4545-81bd-b72105f5cfcd"), Guid.Parse("2a9f1b22-1854-43f2-a30b-c34d3d70f2c9")),
                new Nurse(Guid.Parse("602a9ee4-544b-4df0-a15b-660c55384994"), "Jolanta", "Risso", Guid.Parse("5948ec93-f4ae-4d29-a7ac-4871191b3a2c"), Guid.Parse("b09e2452-867a-41ea-9b95-b00232329e77")),
                new Nurse(Guid.Parse("0b03a188-e01e-495c-aa05-823cd70d53d9"), "Paulina", "Lasek", Guid.Parse("5948ec93-f4ae-4d29-a7ac-4871191b3a2c"), Guid.Parse("6ce705e0-5c6e-400f-b321-6c961206c7cb")),
            };
            
            dbContext.Nurses.AddRange(newNurses);
            await dbContext.SaveChangesAsync();
        }
    }
}