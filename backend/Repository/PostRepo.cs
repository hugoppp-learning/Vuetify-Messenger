using AutoMapper;
using backend.Model;
using backend.Services;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace backend.Repository;

public class PostRepo
{
    private readonly IMapper _mapper;
    private readonly CosmosDbService _db;
    private readonly UserRepo _users;

    public PostRepo(IMapper mapper, CosmosDbService db, UserRepo users)
    {
        _mapper = mapper;
        _db = db;
        _users = users;
    }


    public Task Add(Post post)
    {
        var dbPost = _mapper.Map<DbPost>(post);
        return _db.Container.CreateItemAsync(dbPost);
    }

    public async Task<IEnumerable<Post>> GetAll(ApplicationUser currentUser)
    {
        var allCurrentUserLikes = (await _db.Container.GetItemLinqQueryable<DbPostInteraction>()
            .Where(item => item.Discriminator == Discriminator.PostInteraction
                           && item.InteractionType == PostInteractionType.Like
                           && item.InteractionUserId == currentUser.Id)
            .Select(item => item.PostId)
            .ToFeedIterator().ReadNextAsync()).ToHashSet();

        var allPosts = await _db.Container.GetItemLinqQueryable<DbPost>()
            .Where(u => u.Discriminator == Discriminator.Post)
            .ToFeedIterator().ReadNextAsync();


        foreach (var post in allPosts)
        {
            post.Liked = allCurrentUserLikes.Contains(post.Id);
        }

        return allPosts;
    }

    public Post GetById(Guid id)
    {
        var post = _db.Container.GetItemLinqQueryable<DbPost>()
            .Where(post => post.Id == id)
            .ToFeedIterator().ReadNextAsync().Result.First();

        return post;
    }


    public void Delete(Guid id)
    {
        _db.Container.DeleteItemAsync<DbPost>(id.ToString(), new PartitionKey(id.ToString()));
    }

    public string? GetPosterUsername(Guid id)
    {
        var posterUsername = _db.Container.GetItemLinqQueryable<DbPost>()
            .Where(post => post.Id == id)
            .Select(post => post.Username)
            .ToFeedIterator().ReadNextAsync().Result.FirstOrDefault();

        return posterUsername;
    }

    public async Task AddPostInteraction(PostInteraction postInteraction)
    {
        var dbPostInteraction = _mapper.Map<DbPostInteraction>(postInteraction);

        if (0 != await _db.Container
                .GetItemLinqQueryable<PostInteraction>()
                .Where(item => item.PostId == postInteraction.PostId &&
                               item.InteractionUserId == postInteraction.InteractionUserId &&
                               item.InteractionType == postInteraction.InteractionType)
                .CountAsync())
            return;

        await _db.Container.CreateItemAsync(dbPostInteraction);

        await _db.Container.PatchItemAsync<DbPost>(
            postInteraction.PostId.ToString(), new PartitionKey(postInteraction.PostId.ToString()),
            new[] { PatchOperation.Increment("/likesCount", 1) }
        );
    }

    public async Task RemovePostInteraction(Post post, ApplicationUser user, PostInteractionType interactionType)
    {
        var interactionId = (await _db.Container
            .GetItemLinqQueryable<DbPostInteraction>()
            .Where(item => item.PostId == post.Id &&
                           item.InteractionUserId == user.Id &&
                           item.InteractionType == interactionType
            )
            .Select(item => item.Id)
            .ToFeedIterator().ReadNextAsync()).Single().ToString();

        await _db.Container
            .DeleteItemAsync<PostInteraction>(interactionId, new PartitionKey(interactionId));

        await _db.Container.PatchItemAsync<DbPost>(post.Id.ToString(), new PartitionKey(post.Id.ToString()),
            new[] { PatchOperation.Increment("/likesCount", -1) });
    }
}