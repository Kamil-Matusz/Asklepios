using Asklepios.Core.Entities.Users;
using Asklepios.Infrastructure.DAL.PostgreSQL;
using Microsoft.EntityFrameworkCore;

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
                new Nurse(Guid.Parse("576c9de7-468f-4d67-b15e-b6b93dcbc0a5"), "Jolanta", "Risso", Guid.Parse("6ce705e0-5c6e-400f-b321-6c961206c7cb"), Guid.Parse("b09e2452-867a-41ea-9b95-b00232329e77")),
                new Nurse(Guid.Parse("602a9ff4-544b-4df0-a15b-660c55384994"), "Barbara", "Nowak", Guid.Parse("bb4a0fcf-81b7-43b9-9cdd-d78cf89f6f2c"), Guid.Parse("cd77e1b0-caf8-4be1-8edd-6e9793a4b474")),
                new Nurse(Guid.Parse("f567d6c1-214b-4abd-a654-9b102f6f4bc1"), "Marta", "Piotrowska", Guid.Parse("a4c69f2b-dcf3-4e8b-baa6-1275122c4e68"), Guid.Parse("566a7ec8-8a03-4cc8-ac58-18e9815bb2c1"))
            };
            
            foreach (var nurse in newNurses)
            {
                dbContext.Nurses.Add(nurse);
                await dbContext.SaveChangesAsync();
            }
            
        }
    }
}