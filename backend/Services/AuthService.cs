using backend.Controllers;
using backend.Repository;
using static BCrypt.Net.BCrypt;

namespace backend.Services;

public class AuthService
{
    private readonly ILogger<AuthService> _logger;
    private readonly UserRepo _users;
    private readonly ApplicationJwtConfig _jwtConfig;


    public AuthService(ILogger<AuthService> logger, UserRepo users, ApplicationJwtConfig jwtConfig)
    {
        _logger = logger;
        _users = users;
        _jwtConfig = jwtConfig;
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
        return _jwtConfig.GenerateEmailVerificationToken(signupDto);
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

    public bool ValidateEmail(string token)
    {
        var validTokenData = _jwtConfig.ValidateEmailToken(token);
        if (validTokenData is null)
            return false;
        
        var applicationUser = _users.FindByUsername(validTokenData.Value.Username);

        if (applicationUser is null || applicationUser.Roles.Contains(Role.Verified))
            return false;
        
        applicationUser.Roles.Add(Role.Verified);
        return true;
    }
}