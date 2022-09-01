using backend.Model;

namespace backend.Services;

public class LoggerEmailService : IEmailSendingService
{
    private readonly ILogger<LoggerEmailService> _logger;

    public LoggerEmailService(ILogger<LoggerEmailService> logger)
    {
        _logger = logger;
    }

    public Task SendEmailVerification(ApplicationUser user, string emailVerificationToken)
    {
        _logger.LogInformation("Email would be send to {} with token: '{EmailVerificationToken}'",
            user.Email, emailVerificationToken);
        return Task.CompletedTask;
    }
}