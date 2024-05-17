using System.Net;
using Asklepios.Application.Services.Email.SendGrid;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Asklepios.Application.Services.Email;

public class EmailService : IEmailService
{
    private readonly string _sendGridKey;

    public EmailService(IConfiguration configuration)
    {
        _sendGridKey = configuration["sendGridKey"];
    }
    
    public async Task SendEmailAboutGenerateAccountAsync(string recipientEmail, string password)
    {
        var client = new SendGridClient(_sendGridKey);
        var to = new EmailAddress(recipientEmail);
        var from = new EmailAddress("asklepios@op.pl", "Asklepios-System");

        var message = MailHelper.CreateSingleTemplateEmail(from, to,"d-842d31878e374b30a33cd4d93a91fb37", new
        {
            Email = recipientEmail,
            Password = password
        });
        
        var response = await client.SendEmailAsync(message);
        
        if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Accepted)
        {
            throw new Exception($"Error code: {response.StatusCode}");
        }
    }

    public async Task SendEmailWithHelloMessageAsync(string recipientEmail)
    {
        var client = new SendGridClient(_sendGridKey);
        var to = new EmailAddress(recipientEmail);
        var from = new EmailAddress("asklepios@op.pl", "Asklepios-System");
        
        var message = MailHelper.CreateSingleTemplateEmail(from, to,"d-f97ede562c644e99be6fcd950e9fa511", new
        {
            Email = recipientEmail,
        });
        
        var response = await client.SendEmailAsync(message);
        
        if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Accepted)
        {
            throw new Exception($"Error code: {response.StatusCode}");
        }
    }
}