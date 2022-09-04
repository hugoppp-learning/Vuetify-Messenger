using backend.Controllers;
using backend.Model;
using backend.Repository;
using static BCrypt.Net.BCrypt;

namespace backend.Services.Security;

public class AuthService
{
    private readonly ILogger<AuthService> _logger;
    private readonly UserRepo _users;
    private readonly JwtEmailVerificationService _jwtEmailVerification;
    private readonly IEmailSendingService _emailSendingService;


    public AuthService(ILogger<AuthService> logger, UserRepo users, JwtEmailVerificationService jwtEmailVerification,
        IEmailSendingService emailSendingService)
    {
        _logger = logger;
        _users = users;
        _jwtEmailVerification = jwtEmailVerification;
        _emailSendingService = emailSendingService;
    }

    public async Task SignupAsync(SignupDto signupDto)
    {
        var applicationUser = new ApplicationUser
        {
            Email = signupDto.Email,
            PasswordHash = HashPassword(signupDto.Password),
            Username = signupDto.Username,
            Roles = new List<Role>(),
            Id = Guid.NewGuid()
        };
        await _users.Add(applicationUser);
        var emailVerificationToken = _jwtEmailVerification.GenerateEmailVerificationToken(signupDto);
        await _emailSendingService.SendEmailVerification(applicationUser, emailVerificationToken);
    }


    public async Task<ApplicationUser?> Authenticate(AuthRequestDto login)
    {
        var applicationUser = await _users.FindByUsername(login.Username);
        if (applicationUser is null)
            return null;

        if (Verify(login.Password, applicationUser.PasswordHash))
            return applicationUser;

        return null;
    }

    public async Task<bool> VerifyEmail(string token)
    {
        var email = _jwtEmailVerification.ValidateEmailToken(token);
        if (email is null)
            return false;

        var applicationUser = await _users.FindByEmail(email);

        if (applicationUser is null || applicationUser.Roles.Contains(Role.Verified))
            return false;

        applicationUser.Roles.Add(Role.Verified);
        await _users.UpdateRoles(applicationUser.Id, applicationUser.Roles);
        return true;
    }
}