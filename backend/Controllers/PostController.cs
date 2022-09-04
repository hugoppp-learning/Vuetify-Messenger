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
    private readonly PostRepo _posts;

    public PostController(ILogger<PostController> logger, UserRepo users, PostRepo posts)
    {
        _logger = logger;
        _users = users;
        _posts = posts;
    }


    [HttpPost]
    public IActionResult Post([FromBody] CreatePostDto createPost)
    {
        var applicationUser = _users.FromHttpContext(HttpContext);

        var post = new Post()
        {
            Message = createPost.Message,
            Username = applicationUser.Username,
            ProfilePicture = applicationUser.ProfilePicture,
            // Id = new Guid(),//todo
        };
        _posts.Add(post);

        return Ok(PostDto.New(post));
    }

    [HttpDelete("{id:Guid}/")]
    public IActionResult Delete(Guid id)
    {
        var applicationUser = _users.FromHttpContext(HttpContext);
        var post = _posts.GetById(id);
        if (post is null)
            return NotFound();
        if (applicationUser.Username != post.Username)
            return Unauthorized();

        _posts.Delete(id);
        return Ok();
    }

    [HttpGet]
    [AllowAnonymous]
    public ActionResult<IEnumerable<PostDto>> Get()
    {
        var posts = _posts.GetAll();
        var postDtos = posts.Select(p => new PostDto(p, _users.FromHttpContext(HttpContext).Id));
        return Ok(postDtos);
    }

    [HttpPost("{id:Guid}/Like")]
    public IActionResult AddLike(Guid id)
    {
        var (post, user) = AssertPostNotFromCurrentUser(id);
        post.LikedUserIds.Add(user.Id);
        return Ok();
    }

    [HttpDelete("{id:Guid}/Like")]
    public IActionResult RemoveLike(Guid id)
    {
        var (post, user) = AssertPostNotFromCurrentUser(id);
        post.LikedUserIds.Remove(user.Id);
        return Ok();
    }

    private (Post, ApplicationUser) AssertPostNotFromCurrentUser(Guid id)
    {
        var post = _posts.GetById(id);
        var applicationUser = _users.FromHttpContext(HttpContext);
        if (applicationUser.Username == post.Username)
            throw new InvalidOperationException("Action not supported for posts by the user itself");

        return (post, applicationUser);
    }
}