using Asklepios.Core.Entities.Patients;
using Asklepios.Infrastructure.DAL.PostgreSQL;

namespace Asklepios.Infrastructure.DAL.Seeders.Patients;

public class PatientsSeeder : IOrderedSeeder
{
    public int Order => 7;
    public async Task SeedAsync(AsklepiosDbContext dbContext)
    {
        var patients = dbContext.Patients.ToList();
        if (!patients.Any())
        {
            var newPatients = new List<Patient>()
            {
                new Patient(Guid.Parse("bc06de0b-ca04-4341-a2df-492bc4f6cecf"), "Konrad", "Bieniasz", "01300406346", "Zawał serca", false, "Operacja", Guid.Parse("2a9f1b22-1854-43f2-a30b-c34d3d70f2c9"), Guid.Parse("c1058167-d03e-4086-ac83-7c6f183a010d"), Guid.Parse("e582299d-1a49-4d7b-8e36-eadb449dd209"), DateOnly.FromDateTime(DateTime.Today), "Akacjowa 24 35-211 Rzeszów"),
                new Patient(Guid.Parse("bc06de0b-ca04-4341-a2af-492bc4f6c5ba"), "Michał", "Jaros", "01300405346", "Wszczepie bajpasów", false, "Operacja", Guid.Parse("2a9f1b22-1854-43f2-a30b-c34d3d70f2c9"), Guid.Parse("c1058167-d03e-4086-ac83-7c6f183a010d"), Guid.Parse("e582299d-1a49-4d7b-8e36-eadb449dd209"), DateOnly.FromDateTime(DateTime.Today), "Wojska Polskiego 12 36-040 Boguchwała"),
            };
            
            dbContext.Patients.AddRange(newPatients);
            await dbContext.SaveChangesAsync();
        }
    }
}