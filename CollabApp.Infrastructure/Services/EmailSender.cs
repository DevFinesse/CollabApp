using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;

namespace CollabApp.Infrastructure.Services;

public class EmailSender : IEmailSender
{
    private readonly ILogger<EmailSender> _logger;

    public EmailSender(ILogger<EmailSender> logger)
    {
        _logger = logger;
    }

    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
 _logger.LogInformation("Email: {email}, Subject: {subject}, Message: {message}", email, subject, htmlMessage);
      // TODO: Implement actual email sending logic
        return Task.CompletedTask;
    }
}