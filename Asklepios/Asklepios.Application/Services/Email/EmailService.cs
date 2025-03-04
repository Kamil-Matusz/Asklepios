using System.Net;
using Asklepios.Application.Services.Email.SendGrid;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Asklepios.Application.Services.Email;

public class EmailService : IEmailService
{
    private readonly SendGridClient _client;
    private readonly EmailAddress _from;

    public EmailService(SendGridOptions sendGridOptions)
    {
        _client = new SendGridClient(sendGridOptions.keySensGrid);
        _from = new EmailAddress("asklepios@op.pl", "Asklepios-System");
    }

    private async Task SendEmailAsync(string recipientEmail, string templateId, object templateData)
    {
        var to = new EmailAddress(recipientEmail);
        var message = MailHelper.CreateSingleTemplateEmail(_from, to, templateId, templateData);
        var response = await _client.SendEmailAsync(message);

        if (response.StatusCode is not HttpStatusCode.OK and not HttpStatusCode.Accepted)
        {
            throw new Exception($"Error code: {response.StatusCode}");
        }
    }

    public Task SendEmailAboutGenerateAccountAsync(string recipientEmail, string password) =>
        SendEmailAsync(recipientEmail, "d-7de0be31f14a4765a8c9e0878bdaa329", new { Email = recipientEmail, Password = password });

    public Task SendEmailWithHelloMessageAsync(string recipientEmail) =>
        SendEmailAsync(recipientEmail, "d-421ef45c8f6747298210d77560d0b620", new { Email = recipientEmail });

    public Task SendEmailWithConfirmVisitAsync(string recipientEmail, string patientName, string patientSurname, string appointmentDate, string doctorName, string doctorSurname) =>
        SendEmailAsync(recipientEmail, "d-101e0335ed5043b491b3e6a71f3b9b69", new 
        { 
            Email = recipientEmail, 
            PatientName = patientName, 
            PatientSurname = patientSurname, 
            AppointmentDate = appointmentDate, 
            DoctorName = doctorName, 
            DoctorSurname = doctorSurname 
        });

    public Task SendEmailWithReminderAboutVisitAsync(string recipientEmail, string patientName, string patientSurname, string appointmentDate, string doctorName, string doctorSurname) =>
        SendEmailAsync(recipientEmail, "d-ee756142f76e49f285a5cf2e87fc27fb", new 
        { 
            Email = recipientEmail, 
            PatientName = patientName, 
            PatientSurname = patientSurname, 
            AppointmentDate = appointmentDate, 
            DoctorName = doctorName, 
            DoctorSurname = doctorSurname 
        });
}