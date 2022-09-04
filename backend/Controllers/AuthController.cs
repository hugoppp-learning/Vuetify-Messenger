using backend.Model;
using backend.Repository;
using backend.Services.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly UserRepo _users;
    private readonly JwtLoginTokenService _jwtLogin;
    private readonly ILogger<AuthController> _logger;

    public AuthController(AuthService authService, UserRepo users,
        ILogger<AuthController> logger, JwtLoginTokenService jwtLogin)
    {
        _authService = authService;
        _users = users;
        _logger = logger;
        _jwtLogin = jwtLogin;
    }

    [HttpPost("auth")]
    public ActionResult<AuthResponse> Auth([FromBody] AuthRequestDto login)
    {
        var user = _authService.Authenticate(login);
        if (user is null)
            return Ok(new AuthResponse(AuthResponseCode.InvalidCredentials));

        if (!user.Roles.Contains(Role.Verified))
            return Ok(new AuthResponse(AuthResponseCode.NotVerified));

        string token = _jwtLogin.GenerateSignInToken(user);
        return Ok(new AuthResponse(AuthResponseCode.Ok, token));
    }


    [HttpPost("register")]
    public async Task<ActionResult<SignupResponse>> Signup([FromBody] SignupDto signup)
    {
        if (_users.FindByUsername(signup.Username) is not null)
            return Ok(new SignupResponse(SignupResponseCode.UserNameTaken));
        if (_users.FindByEmail(signup.Email) is not null)
            return Ok(new SignupResponse(SignupResponseCode.EmailTaken));

        await _authService.SignupAsync(signup);
        return Ok(new SignupResponse(SignupResponseCode.Ok));
    }

    [HttpPost("VerifyEmail")]
    public ActionResult<VerifyEmailResponse> VerifyEmail(string token)
    {
        if (_authService.VerifyEmail(token))
            return Ok(new VerifyEmailResponse(VerifyEmailResponseCode.Ok));
        else
            return Ok(new VerifyEmailResponse(VerifyEmailResponseCode.Invalid));
    }

}