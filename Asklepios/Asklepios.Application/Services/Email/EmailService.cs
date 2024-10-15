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
    
    public async Task SendEmailAboutGenerateAccountAsync(string recipientEmail, string password)
    {
        var client = new SendGridClient(_sendGridKey);
        var to = new EmailAddress(recipientEmail);
        var from = new EmailAddress("asklepios@op.pl", "Asklepios-System");

        var message = MailHelper.CreateSingleTemplateEmail(from, to,"d-7de0be31f14a4765a8c9e0878bdaa329", new
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

    public async Task SendEmailWithConfirmVisit(string recipientEmail, string patientName, string patientSurname,
        string appointmentDate)
    {
        var client = new SendGridClient(_sendGridKey);
        var to = new EmailAddress(recipientEmail);
        var from = new EmailAddress("asklepios@op.pl", "Asklepios-System");
        
        var message = MailHelper.CreateSingleTemplateEmail(from, to,"d-101e0335ed5043b491b3e6a71f3b9b69", new
        {
            Email = recipientEmail,
            PatientName = patientName,
            PatientSurname = patientSurname,
            AppointmentDate = appointmentDate
        });
        
        var response = await client.SendEmailAsync(message);
        
        if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Accepted)
        {
            throw new Exception($"Error code: {response.StatusCode}");
        }
    }
}