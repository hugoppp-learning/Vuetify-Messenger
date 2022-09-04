using backend.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly UserRepo _userRepo;


    public UserController(ILogger<UserController> logger, UserRepo userRepo)
    {
        _logger = logger;
        _userRepo = userRepo;
    }


    [HttpGet("current")]
    public ActionResult<UserDto> GetCurrent()
    {
        return new UserDto(_userRepo.FromHttpContext(HttpContext));
    }
}