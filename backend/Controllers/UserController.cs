using backend.Model;
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

    public record UserDto(string Email, List<Role> Roles, string Username, string ProfilePicture)
    {
        public UserDto(ApplicationUser au) : this(au.Email, au.Roles, au.Username, au.ProfilePicture)
        {
        }
    };

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