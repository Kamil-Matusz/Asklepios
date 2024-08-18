namespace Asklepios.Application.Services.Notification;

public interface INotificationService
{
    Task NotifyDoctorAboutNewPatient(Guid doctorId, string message);
}