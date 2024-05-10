using System.Net;
using Asklepios.Application.Services.Email.SendGrid;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Asklepios.Application.Services.Email;

public class EmailService : IEmailService
{
    private readonly string _sendGridKey;

    public EmailService(SendGridOptions sendGridOptions)
    {
        _sendGridKey = sendGridOptions.keySensGrid;
    }
    
    public async Task SendEmailAsync(string recipientEmail, string password)
    {
        var client = new SendGridClient(_sendGridKey);
        var to = new EmailAddress(recipientEmail);
        var from = new EmailAddress("asklepios@op.pl", "Asklepios-System");
        var subject = "Administrator wygenerował konto dla Ciebie";
        var htmlContent = $"<strong>Twoje hasło:</strong> {password}";

        var message = MailHelper.CreateSingleEmail(from, to, subject, null, htmlContent);
        
        var response = await client.SendEmailAsync(message);
        
        if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Accepted)
        {
            throw new Exception($"Wystąpił błąd podczas wysyłania e-maila: {response.StatusCode}");
        }
    }
}