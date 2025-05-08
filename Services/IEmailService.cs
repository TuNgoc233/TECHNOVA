using TECHNOVA.Services;

namespace TECHNOVA.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string htmlContent, string plainTextContent);
    }
}
