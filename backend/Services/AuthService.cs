using backend.Controllers;
using backend.Repository;
using backend.Services.Security;
using static BCrypt.Net.BCrypt;

namespace backend.Services;

public class AuthService
{
    private readonly ILogger<AuthService> _logger;
    private readonly UserRepo _users;
    private readonly JwtEmailVerificationService _jwtEmailVerification;


    public AuthService(ILogger<AuthService> logger, UserRepo users, JwtEmailVerificationService jwtEmailVerification)
    {
        _logger = logger;
        _users = users;
        _jwtEmailVerification = jwtEmailVerification;
    }

    public string Signup(SignupDto signupDto)
    {
        _users.Add(new ApplicationUser
        {
            Email = signupDto.Email,
            PasswordHash = HashPassword(signupDto.Password),
            Username = signupDto.Username,
            Roles = new List<Role>()
        });
        //todo send email
        return _jwtEmailVerification.GenerateEmailVerificationToken(signupDto);
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

    public bool VerifyEmail(string token)
    {
        var email = _jwtEmailVerification.ValidateEmailToken(token);
        if (email is null)
            return false;
        
        var applicationUser = _users.FindByEmail(email);

        if (applicationUser is null || applicationUser.Roles.Contains(Role.Verified))
            return false;
        
        applicationUser.Roles.Add(Role.Verified);
        return true;
    }
}