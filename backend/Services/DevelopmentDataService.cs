using backend.Controllers;

namespace backend.Services;

public class DevelopmentDataService
{
    private readonly AuthService _authService;

    public DevelopmentDataService(AuthService authService)
    {
        _authService = authService;
    }

    public void SeedData()
    {
        CreateTestAccount();
    }

    private void CreateTestAccount()
    {
        var token = _authService.Signup(new SignupDto("a@b.c", "string", "string"));
        if (!_authService.ValidateEmail(token))
            throw new Exception("Could not create test account");
    }
}