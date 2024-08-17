using Microsoft.AspNetCore.SignalR;

namespace Asklepios.Infrastructure.SignalR;

public class HospitalHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}