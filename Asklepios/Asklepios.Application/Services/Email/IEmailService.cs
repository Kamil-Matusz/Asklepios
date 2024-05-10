namespace Asklepios.Application.Services.Email;

public interface IEmailService
{
    Task SendEmailAsync(string recipientEmail, string password);
}