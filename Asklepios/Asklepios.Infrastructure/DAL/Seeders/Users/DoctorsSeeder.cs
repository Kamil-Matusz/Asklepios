using Asklepios.Core.Entities.Users;
using Asklepios.Infrastructure.DAL.PostgreSQL;

namespace Asklepios.Infrastructure.DAL.Seeders.Users;

public class DoctorsSeeder : IOrderedSeeder
{
    public int Order => 5;
    
    public async Task SeedAsync(AsklepiosDbContext dbContext)
    {
        var doctors = dbContext.MedicalStaff.ToList();
        if (!doctors.Any())
        {
            var newDoctors = new List<MedicalStaff>()
            {
                new MedicalStaff(Guid.Parse("e582299d-1a49-4d7b-8e36-eadb449dd209"), "Kamil", "Matusz", "+48882101040", "+48123456213", "Kardiolog", "ABC123", Guid.Parse("c4377bd0-73f3-4487-be66-e22557b7c76e"), Guid.Parse("2a9f1b22-1854-43f2-a30b-c34d3d70f2c9")),
                new MedicalStaff(Guid.Parse("e582299d-1a49-4d7b-8e36-eadb449dd212"), "Miłosz", "Michalski", "+48602123456", "+48567432145", "Ortopeda", "ABC236", Guid.Parse("91fc545f-4732-419f-812a-9e9603b515c6"), Guid.Parse("98113a7d-371a-4561-84e0-bf664e9e8031")),
                new MedicalStaff(Guid.Parse("c9e7f8b6-3f8d-49e7-bcd1-4f01e9f8ddef"), "Andrzej", "Kowalski", "+48987654321", "+48734567812", "Ortopeda", "LMN123", Guid.Parse("56efc67d-9d7a-441d-88a4-4b5e63b27ef7"), Guid.Parse("98113a7d-371a-4561-84e0-bf664e9e8031")),
                new MedicalStaff(Guid.Parse("c9e7f8b6-3f8d-49e7-bcd1-4e02e9f8ddef"), "Grzegorz", "Adamczyk", "+48987654321", "+48734567812", "Kardiolog", "LMN123", Guid.Parse("ae9e2a5d-7aa4-47ad-bbdd-768c8bdf4a64"), Guid.Parse("2a9f1b22-1854-43f2-a30b-c34d3d70f2c9")),
            };
            
            dbContext.MedicalStaff.AddRange(newDoctors);
            await dbContext.SaveChangesAsync();
        }
    }
    
}