using AutoMapper;
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
    private readonly IMapper _mapper;

    public PostController(ILogger<PostController> logger, UserRepo users, PostRepo posts, IMapper mapper)
    {
        _logger = logger;
        _users = users;
        _posts = posts;
        _mapper = mapper;
    }


    [HttpPost]
    public async Task<ActionResult<PostDto>> Post([FromBody] CreatePostDto createPost)
    {
        var applicationUser = await _users.FromHttpContext(HttpContext);

        var post = new Post()
        {
            Message = createPost.Message,
            Username = applicationUser.Username,
            ProfilePicture = applicationUser.ProfilePicture,
            Liked = false,
            LikesCount = 0,
        };
        await _posts.Add(post);

        return Ok(_mapper.Map<PostDto>(post));
    }

    [HttpDelete("{id:Guid}/")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var applicationUser = await _users.FromHttpContext(HttpContext);
        var post = await _posts.GetById(id);
        if (post is null)
            return NotFound();
        if (applicationUser.Username != post.Username)
            return Unauthorized();

        await _posts.Delete(id);
        return Ok();
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<PostDto>>> GetPosts()
    {
        var posts = (await _posts.GetAll(await _users.FromHttpContext(HttpContext))).ToList();
        return Ok(_mapper.Map<List<PostDto>>(posts));
    }

    [HttpPost("{id:Guid}/Like")]
    public async Task<IActionResult> AddLike(Guid id)
    {
        var (post, currentUser) = await AssertPostNotFromCurrentUser(id);
        var postInteraction = new PostInteraction()
        {
            InteractionType = PostInteractionType.Like,
            PostId = post.Id,
            InteractionUserId = currentUser.Id
        };
        await _posts.AddPostInteraction(postInteraction);
        return Ok();
    }

    [HttpDelete("{id:Guid}/Like")]
    public async Task<IActionResult> RemoveLike(Guid id)
    {
        var (post, user) = await AssertPostNotFromCurrentUser(id);
        await _posts.RemovePostInteraction(post, user, PostInteractionType.Like);
        return Ok();
    }

    private async Task<(Post, ApplicationUser)> AssertPostNotFromCurrentUser(Guid id)
    {
        var post = await _posts.GetById(id);
        var applicationUser = await _users.FromHttpContext(HttpContext);
        if (applicationUser.Username == post.Username)
            throw new InvalidOperationException("Action not supported for posts by the user itself");

        return (post, applicationUser);
    }
}