using Asklepios.Core.Entities.Clinics;
using Asklepios.Core.ValueObjects;
using Asklepios.Infrastructure.DAL.PostgreSQL;

namespace Asklepios.Infrastructure.DAL.Seeders.Clinics;

public class ClinicAppointmentsSeeder : IOrderedSeeder
{
    public int Order => 9;
    public async Task SeedAsync(AsklepiosDbContext dbContext)
    {
        var clinicAppointments = dbContext.ClinicAppointments.ToList();
        if (!clinicAppointments.Any())
        {
            var newClinicAppointments = new List<ClinicAppointment>()
            {
                new ClinicAppointment(
                    Guid.NewGuid(),
                    new DateTime(2024, 12, 2, 12, 0, 0),
                    AppointmentType.Consultation(),
                    Guid.Parse("db85f588-2250-4479-b826-7c210bbaafac"),
                    Guid.Parse("e582299d-1a49-4d7b-8e36-eadb449dd209"),
                    "Scheduled"
                ),
                new ClinicAppointment(
                    Guid.NewGuid(),
                    new DateTime(2024, 01, 14, 10, 0, 0),
                    AppointmentType.Consultation(),
                    Guid.Parse("db85f588-2250-4479-b826-7c210bbaafac"),
                    Guid.Parse("e582299d-1a49-4d7b-8e36-eadb449dd209"),
                    "Scheduled"
                ),
                new ClinicAppointment(
                    Guid.NewGuid(),
                    new DateTime(2024, 02, 12, 12, 0, 0),
                    AppointmentType.Consultation(),
                    Guid.Parse("db84f587-2250-4479-b826-7c210bbaafac"),
                    Guid.Parse("e582299d-1a49-4d7b-8e36-eadb449dd209"),
                    "Scheduled"
                )
            };
        }
    }
}