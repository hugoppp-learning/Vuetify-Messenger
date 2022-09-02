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

    public record PostDto(int Id, string Username, string? ProfilePicture, string Message, int Likes, bool Liked)
    {
        public PostDto(Post post,
            int currentUserId) : this(
            post.Id,
            post.Username,
            post.ProfilePicture,
            post.Message,
            post.LikedUserIds.Count,
            post.LikedUserIds.Contains(currentUserId))
        {
        }

        public static PostDto New(Post post)
        {
            return new PostDto(post.Id, post.Username, post.ProfilePicture, post.Message, 0, false);
        }
    };

    public PostController(ILogger<PostController> logger, UserRepo users)
    {
        _logger = logger;
        _users = users;
    }

    public record CreatePostDto(string Message);

    [HttpPost]
    public IActionResult Post([FromBody] CreatePostDto createPost)
    {
        var applicationUser = _users.FromHttpContext(HttpContext);

        var post = new Post()
        {
            Message = createPost.Message,
            Username = applicationUser.Username,
            ProfilePicture = applicationUser.ProfilePicture,
            Id = _posts.NextId(),
        };
        _posts.Add(post);

        return Ok(PostDto.New(post));
    }

    [HttpGet]
    [AllowAnonymous]
    public ActionResult<IEnumerable<PostDto>> Get()
    {
        Task.Delay(TimeSpan.FromSeconds(1)).Wait();
        return Ok(_posts
            .OrderByDescending(p => p.Id)
            .Select(p => new PostDto(p, _users.FromHttpContext(HttpContext).Id)));
    }

    [HttpPost("{id:int}/Like")]
    public IActionResult AddLike(int id)
    {
        var (post, user) = AssertPostNotFromCurrentUser(id);
        post.LikedUserIds.Add(user.Id);
        return Ok();
    }

    [HttpDelete("{id:int}/Like")]
    public IActionResult RemoveLike(int id)
    {
        var (post, user) = AssertPostNotFromCurrentUser(id);
        post.LikedUserIds.Remove(user.Id);
        return Ok();
    }

    private (Post, ApplicationUser) AssertPostNotFromCurrentUser(int id)
    {
        var post = _posts.First(p => p.Id == id);
        var applicationUser = _users.FromHttpContext(HttpContext);
        if (applicationUser.Username == post.Username)
            throw new InvalidOperationException("Action not supported for posts by the user itself");
        return (post, applicationUser);
    }

    private static readonly List<Post> _posts = new()
    {
        new Post()
        {
            Id = 1,
            Username = "hugop",
            ProfilePicture = "https://i.pravatar.cc/300",
            Message = "This is the first post on this site, Yay!! :)",
            LikedUserIds = { 1, 2, 3, 4, 5 }
        },
        new Post()
        {
            Id = 2,
            Username = "hugop",
            ProfilePicture = "https://i.pravatar.cc/300",
            Message = "This is the second post on this site",
            LikedUserIds = { 1, 2, 3, 4, 5 }
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
        },
    };
}