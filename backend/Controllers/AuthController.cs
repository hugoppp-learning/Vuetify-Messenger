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
    private readonly SignupService _signupService;
    private readonly UserRepo _users;
    private readonly ApplicationJwtConfig _jwtConfig;

    public AuthController(SignupService signupService, UserRepo users, ApplicationJwtConfig jwtConfig)
    {
        _signupService = signupService;
        _users = users;
        _jwtConfig = jwtConfig;
    }

    [AllowAnonymous]
    [HttpPost("Auth")]
    public ActionResult<AuthResponse> Auth([FromBody] AuthRequestDto login)
    {
        var user = _signupService.Authenticate(login);
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
    [HttpPost("Register")]
    public ActionResult<SignupResponse> Signup([FromBody] SignupDto signup)
    {
        if (_users.FindByUsername(signup.Username) is not null)
            return Ok(new SignupResponse(SignupResponseCode.UserNameTaken));
        if (_users.FindByEmail(signup.Email) is not null)
            return Ok(new SignupResponse(SignupResponseCode.EmailTaken));

        string token = _jwtConfig.GenerateEmailVerificationToken(signup);
        _signupService.Signup(signup, token);

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
    [HttpPost("verifyEmail")]
    public ActionResult<VerifyEmailResponse> VerifyEmail(string token)
    {
        var validTokenData = _jwtConfig.ValidateEmailToken(token);
        if (validTokenData is null)
            return Ok(new VerifyEmailResponse(VerifyEmailResponseCode.Invalid));
        
        var applicationUser = _signupService.ValidateEmail(validTokenData.Value);

        if (applicationUser is null)
            return Ok(new VerifyEmailResponse(VerifyEmailResponseCode.Invalid));

        return Ok(new VerifyEmailResponse(VerifyEmailResponseCode.Ok));
    }

    public record VerifyEmailResponse(VerifyEmailResponseCode Code);

    public enum VerifyEmailResponseCode
    {
        Invalid,
        Ok,
    }
}