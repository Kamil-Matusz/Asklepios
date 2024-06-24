namespace Asklepios.Application.Services.Email;

public interface IEmailService
{
    Task SendEmailAboutGenerateAccountAsync(string recipientEmail, string password);
    Task SendEmailWithHelloMessageAsync(string recipientEmail);
}