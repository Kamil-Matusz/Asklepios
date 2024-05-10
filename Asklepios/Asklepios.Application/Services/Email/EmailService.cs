using Asklepios.Application.Services.Email.SendGrid;
using Microsoft.Extensions.Configuration;

namespace Asklepios.Application.Services.Email;

public class EmailService : IEmailService
{
    private readonly string _sendGridKey;

    public EmailService(SendGridOptions sendGridOptions)
    {
        _sendGridKey = sendGridOptions.keySensGrid;
    }
    
    public Task SendEmailAsync(string recipientEmail, string password)
    {
        throw new NotImplementedException();
    }
}