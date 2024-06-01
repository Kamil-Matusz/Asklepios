using Asklepios.Core.Entities.Departments;
using Asklepios.Infrastructure.DAL.PostgreSQL;

namespace Asklepios.Infrastructure.DAL.Seeders.Departments;

public class DepartmentsSeeder : IOrderedSeeder
{
    public int Order => 2; 
    
    public async Task SeedAsync(AsklepiosDbContext dbContext)
    {
        var departments = dbContext.Departments.ToList();
        if (!departments.Any())
        {
            var newDepartments = new List<Department>()
            {
                new Department(Guid.Parse("2a9f1b22-1854-43f2-a30b-c34d3d70f2c9"), "Kardiologia", 50, 0),
                new Department(Guid.Parse("b09e2452-867a-41ea-9b95-b00232329e77"), "Chirurgia Ogólna", 50, 0),
                new Department(Guid.Parse("58433a0b-157b-4136-93f7-680fe7c27601"), "Chirurgia Ortopedyczna", 50, 0),
                new Department(Guid.Parse("cd77e1b0-caf8-4be1-8edd-6e9793a4b474"), "Położnictwo i ginekologia", 50, 0),
                new Department(Guid.Parse("146d2583-90e4-4404-bfe5-d3711bfeb4e4"), "SOR", 20, 0),
                new Department(Guid.Parse("98113a7d-371a-4561-84e0-bf664e9e8031"), "Ortopedia", 50, 0),
                new Department(Guid.Parse("be11f847-7397-41c8-84f7-9a61c5cb9326"), "Urologia", 50, 0),
                new Department(Guid.Parse("566a7ec8-8a03-4cc8-ac58-18e9815bb2c1"), "Okulistyka", 50, 0),
                new Department(Guid.Parse("c5c236a1-0342-4c31-b0c9-1ee69ab78e50"), "Geriatria", 50, 0),
                new Department(Guid.Parse("b2a4ef24-0e72-4cb7-aed0-ae3ce1dc2e05"), "Toksykologia", 50, 0),
                new Department(Guid.Parse("972e78dc-6ec1-4336-8ea6-3d9cb981e64f"), "Transplantologia", 50, 0),
            };
            
            dbContext.Departments.AddRange(newDepartments);
            await dbContext.SaveChangesAsync();
        }
    }
}