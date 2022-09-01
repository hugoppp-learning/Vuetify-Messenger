using backend.Model;
using backend.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly ILogger<PostController> _logger;
    private readonly UserRepo _users;

    public PostController(ILogger<PostController> logger, UserRepo users)
    {
        _logger = logger;
        _users = users;
    }

    private static readonly List<Post> _posts = new()
    {
        new Post()
        {
            Id = 1,
            Username = "hugop",
            ProfilePicture = "https://i.pravatar.cc/300",
            Message = "This is the first post on this site, Yay!! :)",
            Likes = 12,
            Liked = true
        },
        new Post()
        {
            Id = 2,
            Username = "hugop",
            ProfilePicture = "https://i.pravatar.cc/300",
            Message = "This is the second post on this site",
            Likes = 1
        },
        new Post()
        {
            Id = 3,
            Username = "hugop",
            ProfilePicture = "https://i.pravatar.cc/300",
            Message = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit,
                sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.Ut enim ad minim veniam,
                quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.Duis aute irure dolor
                in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.Excepteur sint occaecat
                cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum",
            Likes = 0
        },
    };

    public record CreatePostDto(string Message);

    [HttpPost]
    public IActionResult Post([FromBody] CreatePostDto createPost)
    {
        var applicationUser = _users.FromHttpContext(HttpContext);
        if (applicationUser is null)
            return Unauthorized();

        _posts.Add(new Post()
        {
            Message = createPost.Message,
            Username = applicationUser.Username,
            ProfilePicture = applicationUser.ProfilePicture,
            Liked = true,
            Likes = 0,
            Id = 1
        });

        return Ok();
    }

    [HttpGet]
    [AllowAnonymous]
    public IEnumerable<object> Get()
    {
        Task.Delay(TimeSpan.FromSeconds(1)).Wait();
        return _posts;
    }
}