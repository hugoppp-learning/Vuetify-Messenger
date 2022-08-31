using backend.Controllers;
using backend.Repository;
using static BCrypt.Net.BCrypt;

namespace backend.Services;

public class SignupService
{
    private readonly ILogger<SignupService> _logger;
    private readonly UserRepo _users;


    public SignupService(ILogger<SignupService> logger, UserRepo users)
    {
        _logger = logger;
        _users = users;
    }

    public void Signup(SignupDto signupDto, string emailVerificationToken)
    {
        _users.Add(new ApplicationUser
        {
            Email = signupDto.Email,
            PasswordHash = HashPassword(signupDto.Password),
            Username = signupDto.Username,
            Roles = new List<Role>()
        });
        //todo send email
        _logger.LogInformation("This is send per email: '{EmailVerificationToken}'", emailVerificationToken);
    }


    public ApplicationUser? Authenticate(AuthRequestDto login)
    {
        var applicationUser = _users.FindByUsername(login.Username);
        if (applicationUser is null)
            return null;

        if (Verify(login.Password, applicationUser.PasswordHash))
            return applicationUser;

        return null;
    }

    public ApplicationUser? ValidateEmail((string Username, string Email) token)
    {
        var applicationUser = _users.FindByUsername(token.Username);

        if (applicationUser is null || applicationUser.Roles.Contains(Role.Verified))
            return null;
        
        applicationUser.Roles.Add(Role.Verified);
        return applicationUser;
    }
}