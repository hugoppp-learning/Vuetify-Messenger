using System.Diagnostics;
using backend.Controllers;
using backend.Repository;
using backend.Services.Security;

namespace backend.Services;

public class DevelopmentDataService
{
    private readonly AuthService _authService;
    private readonly MockEmailSendingService _mockEmailSendingService;

    public DevelopmentDataService(UserRepo userRepo, ILogger<AuthService> authServiceLogger,
        JwtEmailVerificationService jwtEmailVerificationService)
    {
        _mockEmailSendingService = new MockEmailSendingService();
        _authService = new AuthService(authServiceLogger, userRepo, jwtEmailVerificationService,
            _mockEmailSendingService);
    }

    public void SeedData()
    {
        CreateTestAccount().Wait();
    }

    private async Task CreateTestAccount()
    {
        await _authService.SignupAsync(new SignupDto("a@b.c", "string", "string"));
        Debug.Assert(_mockEmailSendingService.EmailVerificationToken is not null);
        if (!_authService.VerifyEmail(_mockEmailSendingService.EmailVerificationToken))
            throw new Exception("Could not create test account");
    }
}