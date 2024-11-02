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
        
        var message = MailHelper.CreateSingleTemplateEmail(from, to,"d-421ef45c8f6747298210d77560d0b620", new
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
        string appointmentDate, string doctorName, string doctorSurname)
    {
        var client = new SendGridClient(_sendGridKey);
        var to = new EmailAddress(recipientEmail);
        var from = new EmailAddress("asklepios@op.pl", "Asklepios-System");
        
        var message = MailHelper.CreateSingleTemplateEmail(from, to,"d-101e0335ed5043b491b3e6a71f3b9b69", new
        {
            Email = recipientEmail,
            PatientName = patientName,
            PatientSurname = patientSurname,
            AppointmentDate = appointmentDate,
            DoctorName = doctorName,
            DoctorSurname = doctorSurname
        });
        
        var response = await client.SendEmailAsync(message);
        
        if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Accepted)
        {
            throw new Exception($"Error code: {response.StatusCode}");
        }
    }
}