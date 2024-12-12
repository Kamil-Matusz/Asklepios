namespace Asklepios.Application.Hangfire;

public interface IClinicAppointmentsCleanupService
{
    Task RemoveOldAppointments();
}