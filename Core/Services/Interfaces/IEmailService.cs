namespace Core.Services.Interfaces;

public interface IEmailService
{
    Task<bool> SendEmailAsync(string toEmail, string subject, string body, CancellationToken cancellationToken = default);
    Task<bool> SendCredentialsEmailAsync(string toEmail, string userName, string password, CancellationToken cancellationToken = default);
    Task<bool> SendPasswordChangedEmailAsync(string toEmail, string newPassword, CancellationToken cancellationToken = default);
}

