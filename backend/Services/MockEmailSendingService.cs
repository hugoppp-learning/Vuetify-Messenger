namespace backend.Services;

public class MockEmailSendingService : IEmailSendingService
{
    public string? EmailVerificationToken { get; private set; }
    public Task SendEmailVerification(ApplicationUser user, string emailVerificationToken)
    {
        EmailVerificationToken = emailVerificationToken;
        return Task.CompletedTask;
    }

}