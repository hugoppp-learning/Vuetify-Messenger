using System.ComponentModel.DataAnnotations;

namespace backend.Controllers;

public record AuthRequestDto([Required] string Username, [Required] string Password);

public record SignupDto([Required] [EmailAddress] string Email, [Required]
    [StringLength(32, MinimumLength = 4)]
    string Username, [Required]
    [StringLength(32, MinimumLength = 4)]
    string Password);

public record AuthResponse(AuthResponseCode Code, string? Token = null);

public enum AuthResponseCode
{
    NotVerified,
    Ok,
    InvalidCredentials
}

public record SignupResponse(SignupResponseCode Code);

public enum SignupResponseCode
{
    EmailTaken,
    UserNameTaken,
    Ok
}

public record VerifyEmailResponse(VerifyEmailResponseCode Code);

public enum VerifyEmailResponseCode
{
    Invalid,
    Ok,
}