using Asklepios.Core.Repositories.Clinics;

namespace Asklepios.Application.Hangfire;

public class ClinicAppointmentsCleanupService : IClinicAppointmentsCleanupService
{
    private readonly IClinicAppointmentRepository _clinicAppointmentRepository;

    public ClinicAppointmentsCleanupService(IClinicAppointmentRepository clinicAppointmentRepository)
    {
        _clinicAppointmentRepository = clinicAppointmentRepository;
    }

    public async Task RemoveOldAppointments()
    {
        var dateThreshold = DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(-6));
        
        var oldAppointments = await _clinicAppointmentRepository.GetAppointmentsOlderThanAsync(dateThreshold);
        
        foreach (var appointment in oldAppointments)
        {
            _clinicAppointmentRepository.DeleteAppointmentByIdAsync(appointment.AppointmentId);
        }
    }
}