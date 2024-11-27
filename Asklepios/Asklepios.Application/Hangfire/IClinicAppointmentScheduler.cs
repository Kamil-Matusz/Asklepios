namespace Asklepios.Application.Hangfire;

public interface IClinicAppointmentScheduler
{
    Task RemindAboutVisit();
}