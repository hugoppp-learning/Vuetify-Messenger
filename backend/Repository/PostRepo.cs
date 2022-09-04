using AutoMapper;
using backend.Model;
using backend.Repository.Database;
using backend.Services;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace backend.Repository;

public class PostRepo
{
    private readonly IMapper _mapper;
    private readonly CosmosDbService _db;

    public PostRepo(IMapper mapper, CosmosDbService db)
    {
        _mapper = mapper;
        _db = db;
    }


    public Task Add(Post post)
    {
        var dbPost = _mapper.Map<DbPost>(post);
        return _db.Container.CreateItemAsync(dbPost);
    }

    public List<Post> GetAll()
    {
        return _db.Container.GetItemLinqQueryable<DbPost>()
            .Where(u => u.Discriminator == Discriminator.Post)
            .ToFeedIterator().ReadNextAsync().Result.Cast<Post>().ToList();
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
}