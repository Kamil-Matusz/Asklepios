using Asklepios.Core.Entities.Clinics;
using Asklepios.Core.Repositories.Clinics;
using Asklepios.Infrastructure.DAL.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Asklepios.Infrastructure.DAL.Repositories.Clinics;

public class ClinicAppointmentDetailsRepository : IClinicAppointmentDetailsRepository
{
    private readonly AsklepiosDbContext _dbContext;
    private readonly DbSet<ClinicAppointmentDetails> _clinicAppointmentDetails;

    public ClinicAppointmentDetailsRepository(AsklepiosDbContext dbContext)
    {
        _dbContext = dbContext;
        _clinicAppointmentDetails = _dbContext.Set<ClinicAppointmentDetails>();
    }

    public async Task<ClinicAppointmentDetails> GetAppointmentDetailsByAppointmentIdAsync(Guid appointmentId)
        => await _clinicAppointmentDetails
            .Include(x => x.ClinicAppointment)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.AppointmentId == appointmentId);

    public async Task AddAppointmentAsync(ClinicAppointmentDetails clinicAppointmentDetails)
    {
        await _clinicAppointmentDetails.AddAsync(clinicAppointmentDetails);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAppointmentAsync(ClinicAppointmentDetails clinicAppointmentDetails)
    {
        _clinicAppointmentDetails.Update(clinicAppointmentDetails);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAppointmentDetailsAsync(ClinicAppointmentDetails clinicAppointmentDetails)
    {
        _clinicAppointmentDetails.Remove(clinicAppointmentDetails);
        await _dbContext.SaveChangesAsync();
    }
}