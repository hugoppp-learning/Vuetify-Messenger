using backend.Model;

namespace backend.Services;

public interface IEmailSendingService
{
    Task SendEmailVerification(ApplicationUser user, string emailVerificationToken);

}