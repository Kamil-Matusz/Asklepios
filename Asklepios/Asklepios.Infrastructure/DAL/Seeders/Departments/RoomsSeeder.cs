using Asklepios.Core.Entities.Departments;
using Asklepios.Infrastructure.DAL.PostgreSQL;

namespace Asklepios.Infrastructure.DAL.Seeders.Departments;

public class RoomsSeeder : IOrderedSeeder
{
    public int Order => 3; 
    
    public async Task SeedAsync(AsklepiosDbContext dbContext)
    {
        var rooms = dbContext.Rooms.ToList();
        if (!rooms.Any())
        {
            var newRooms = new List<Room>()
            {
                new Room(Guid.Parse("c1058167-d03e-4086-ac83-7c6f183a010d"), 111, "Sala Ogólna", 4,Guid.Parse("2a9f1b22-1854-43f2-a30b-c34d3d70f2c9")),
                new Room(Guid.Parse("46bf3a8a-a229-43a9-a350-77bd2a790ccb"), 112, "Sala Ogólna", 4,Guid.Parse("2a9f1b22-1854-43f2-a30b-c34d3d70f2c9")),
                new Room(Guid.Parse("db62bbc8-5f6f-4225-a66d-43f88ad1a38a"), 211, "Sala Ogólna", 4,Guid.Parse("b09e2452-867a-41ea-9b95-b00232329e77")),
                new Room(Guid.Parse("773cabb1-1129-4881-8ab0-3a2f8d8465e6"), 212, "Sala Ogólna", 4,Guid.Parse("b09e2452-867a-41ea-9b95-b00232329e77")),
                new Room(Guid.Parse("70873ccf-b82c-47b5-9d1c-4f225b891f64"), 311, "Sala Ogólna", 4,Guid.Parse("58433a0b-157b-4136-93f7-680fe7c27601")),
                new Room(Guid.Parse("f5089b11-f2b1-4132-8d28-234535123c37"), 312, "Sala Ogólna", 4,Guid.Parse("58433a0b-157b-4136-93f7-680fe7c27601")),
                new Room(Guid.Parse("3757cc2e-ef57-454f-b3de-c5dbc104c38e"), 411, "Sala Ogólna", 4,Guid.Parse("cd77e1b0-caf8-4be1-8edd-6e9793a4b474")),
                new Room(Guid.Parse("8a8752d2-ae69-4369-a13f-6c580ed0a6cb"), 412, "Sala Ogólna", 4,Guid.Parse("cd77e1b0-caf8-4be1-8edd-6e9793a4b474")),
            };
            
            dbContext.Rooms.AddRange(newRooms);
            await dbContext.SaveChangesAsync();
        }
    }
    
}