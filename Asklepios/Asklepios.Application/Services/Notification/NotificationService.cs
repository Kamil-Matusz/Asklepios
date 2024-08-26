using Asklepios.Application.SignalR;
using Asklepios.Core.Repositories.Users;
using Microsoft.AspNetCore.SignalR;

namespace Asklepios.Application.Services.Notification;

public class NotificationService : INotificationService
{
    private readonly IHubContext<HospitalHub> _hubContext;
    private readonly IMedicalStaffRepository _medicalStaffRepository;

    public NotificationService(IHubContext<HospitalHub> hubContext, IMedicalStaffRepository medicalStaffRepository)
    {
        _hubContext = hubContext;
        _medicalStaffRepository = medicalStaffRepository;
    }

    public async Task NotifyDoctorAboutNewPatient(Guid doctorId, string message)
    {
        var userId = await _medicalStaffRepository.GetUserIdByDoctor(doctorId);
        await _hubContext.Clients.Group(userId.ToString()).SendAsync("ReceiveNotification", message);
    }
}