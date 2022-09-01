using System.ComponentModel.DataAnnotations;
using backend.Repository;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

public record SignupDto([Required] [EmailAddress] string Email, [Required]
    [StringLength(32, MinimumLength = 4)]
    string Username, [Required]
    [StringLength(32, MinimumLength = 4)]
    string Password);

public record AuthRequestDto([Required] string Username, [Required] string Password);

[ApiController]
[Route("")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly UserRepo _users;
    private readonly ApplicationJwtConfig _jwtConfig;
    private readonly ILogger<AuthController> _logger;

    public AuthController(AuthService authService, UserRepo users, ApplicationJwtConfig jwtConfig, ILogger<AuthController> logger)
    {
        _authService = authService;
        _users = users;
        _jwtConfig = jwtConfig;
        _logger = logger;
    }

    [AllowAnonymous]
    [HttpPost("auth")]
    public ActionResult<AuthResponse> Auth([FromBody] AuthRequestDto login)
    {
        var user = _authService.Authenticate(login);
        if (user is null)
            return Unauthorized();

        if (!user.Roles.Contains(Role.Verified))
            return Ok(new AuthResponse(AuthResponseCode.NotVerified));

        string token = _jwtConfig.GenerateSignInToken(user);
        return Ok(new AuthResponse(AuthResponseCode.Ok, token));
    }

    public record AuthResponse(AuthResponseCode Code, string? Token = null);

    public enum AuthResponseCode
    {
        NotVerified,
        Ok,
    }


    [AllowAnonymous]
    [HttpPost("register")]
    public ActionResult<SignupResponse> Signup([FromBody] SignupDto signup)
    {
        if (_users.FindByUsername(signup.Username) is not null)
            return Ok(new SignupResponse(SignupResponseCode.UserNameTaken));
        if (_users.FindByEmail(signup.Email) is not null)
            return Ok(new SignupResponse(SignupResponseCode.EmailTaken));

        string emailVerificationToken = _authService.Signup(signup);
        _logger.LogInformation("This is send per email: '{EmailVerificationToken}'", emailVerificationToken);

        return Ok(new SignupResponse(SignupResponseCode.Ok));
    }

    public record SignupResponse(SignupResponseCode Code);

    public enum SignupResponseCode
    {
        EmailTaken,
        UserNameTaken,
        Ok
    }

    [AllowAnonymous]
    [HttpPost("VerifyEmail")]
    public ActionResult<VerifyEmailResponse> VerifyEmail(string token)
    {
        if (_authService.ValidateEmail(token))
            return Ok(new VerifyEmailResponse(VerifyEmailResponseCode.Ok));
        else
            return Ok(new VerifyEmailResponse(VerifyEmailResponseCode.Invalid));
    }

    public record VerifyEmailResponse(VerifyEmailResponseCode Code);

    public enum VerifyEmailResponseCode
    {
        Invalid,
        Ok,
    }
}