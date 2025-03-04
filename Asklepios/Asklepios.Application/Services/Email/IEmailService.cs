namespace Asklepios.Application.Services.Email;

public interface IEmailService
{
    Task SendEmailAboutGenerateAccountAsync(string recipientEmail, string password);
    Task SendEmailWithHelloMessageAsync(string recipientEmail);
    Task SendEmailWithConfirmVisitAsync(string recipientEmail, string patientName, string patientSurname, string appointmentDate, string doctorName, string doctorSurname);
    Task SendEmailWithReminderAboutVisitAsync(string recipientEmail, string patientName, string patientSurname, string appointmentDate, string doctorName, string doctorSurname);
}