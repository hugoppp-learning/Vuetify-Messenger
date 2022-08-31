using backend.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly ILogger<PostController> _logger;

    private readonly List<object> _posts = new()
    {
        new
        {
            id = 1,
            name = "hugop",
            profilePicture = "https://i.pravatar.cc/300",
            message = "This is the first post on this site, Yay!! :)",
            likes = 12,
            liked = true
        },
        new
        {
            id = 2,
            name = "hugop",
            profilePicture = "https://i.pravatar.cc/300",
            message = "This is the second post on this site",
            likes = 1
        },
        new
        {
            id = 3,
            name = "hugop",
            profilePicture = "https://i.pravatar.cc/300",
            message = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit,
                sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.Ut enim ad minim veniam,
                quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.Duis aute irure dolor
                in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.Excepteur sint occaecat
                cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum",
            likes = 0
        },
    };


    public PostController(ILogger<PostController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public IActionResult Post([FromBody] Post post)
    {
        _posts.Add(post);
        return Ok();
    }

    [HttpGet]
    public IEnumerable<object> Get()
    {
        Task.Delay(TimeSpan.FromSeconds(1)).Wait();
        return _posts;
    }
}