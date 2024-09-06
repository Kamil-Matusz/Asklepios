using Asklepios.Application.Services.Users;
using Asklepios.Core.Exceptions.Users;
using Microsoft.AspNetCore.SignalR;

namespace Asklepios.Application.SignalR;

public class HospitalHub : Hub
{
    private readonly IMedicalStaffService _medicalStaffService;

    public HospitalHub(IMedicalStaffService medicalStaffService)
    {
        _medicalStaffService = medicalStaffService;
    }

    public async Task JoinDoctorGroup(Guid doctorId)
    {
        var userId = await _medicalStaffService.GetUserIdByDoctorAsync(doctorId);
        await Groups.AddToGroupAsync(Context.ConnectionId, userId.ToString());
    }
    
    public async Task NotifyDoctorAboutNewPatient(Guid doctorId, string message)
    {
        var userId = await _medicalStaffService.GetUserIdByDoctorAsync(doctorId);
        if (userId == Guid.Empty)
        {
            throw new UserNotFoundException(userId);
        }
        
        await Clients.Group(userId.ToString()).SendAsync("ReceiveNotification", message);
    }
}