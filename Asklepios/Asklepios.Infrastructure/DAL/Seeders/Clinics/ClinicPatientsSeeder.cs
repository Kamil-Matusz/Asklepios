using Asklepios.Core.Entities.Clinics;
using Asklepios.Infrastructure.DAL.PostgreSQL;

namespace Asklepios.Infrastructure.DAL.Seeders.Clinics;

public class ClinicPatientsSeeder : IOrderedSeeder
{
    public int Order => 8;
    public async Task SeedAsync(AsklepiosDbContext dbContext)
    {
        var clinicPatients = dbContext.ClinicPatients.ToList();
        if (!clinicPatients.Any())
        {
            var newClinicPatients = new List<ClinicPatient>()
            {
                new ClinicPatient(Guid.Parse("db85f588-2250-4479-b826-7c210bbaafac"), "Konrad", "Szkalrek",
                    "01300407231", "+48123456872", "konradszklarek@asklepios.com"),
            };
        }
    }
}