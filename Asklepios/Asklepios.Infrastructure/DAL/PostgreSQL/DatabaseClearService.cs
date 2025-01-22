using Asklepios.Infrastructure.DAL.Seeders;
using Microsoft.EntityFrameworkCore;

namespace Asklepios.Infrastructure.DAL.PostgreSQL;

public class DatabaseClearService : IClearDatabase
{
    public async Task ClearDatabaseAsync(AsklepiosDbContext dbContext)
    {
        dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        
        await dbContext.ClinicAppointments.ExecuteDeleteAsync();
        await dbContext.ClinicPatients.ExecuteDeleteAsync();
        await dbContext.Patients.ExecuteDeleteAsync();
        await dbContext.Examinations.ExecuteDeleteAsync();
        await dbContext.MedicalStaff.ExecuteDeleteAsync();
        await dbContext.Nurses.ExecuteDeleteAsync();
        await dbContext.Rooms.ExecuteDeleteAsync();
        await dbContext.Departments.ExecuteDeleteAsync();
        await dbContext.Users.ExecuteDeleteAsync();
        
        await dbContext.SaveChangesAsync();
    }
}