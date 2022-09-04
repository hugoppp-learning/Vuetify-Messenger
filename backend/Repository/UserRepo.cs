using AutoMapper;
using backend.Model;
using backend.Repository.Database;
using backend.Services;
using backend.Services.Security;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace backend.Repository;

public class UserRepo
{
    private readonly IMapper _mapper;
    private readonly CosmosDbService _db;

    public UserRepo(IMapper mapper, CosmosDbService db)
    {
        _mapper = mapper;
        _db = db;
    }

    public void Add(ApplicationUser applicationUser)
    {
        var dbUser = _mapper.Map<DbApplicationUser>(applicationUser);
        _db.Container.CreateItemAsync(dbUser).Wait();
    }

    public List<ApplicationUser> GetAllUsers()
    {
        return _db.Container.GetItemLinqQueryable<DbApplicationUser>()
            .Where(u => u.Discriminator == Discriminator.User)
            .ToFeedIterator().ReadNextAsync().Result.Cast<ApplicationUser>().ToList();
    }

    public ApplicationUser? FindByUsername(string username)
    {
        var dbUser = _db.Container.GetItemLinqQueryable<DbApplicationUser>()
            .Where(u => u.Discriminator == Discriminator.User && u.Username == username)
            .ToFeedIterator().ReadNextAsync().Result.FirstOrDefault();

        return dbUser;
    }

    public ApplicationUser? FindByEmail(string email)
    {
        var dbUser = _db.Container.GetItemLinqQueryable<DbApplicationUser>()
            .Where(u => u.Discriminator == Discriminator.User && u.Email == email)
            .ToFeedIterator().ReadNextAsync().Result.FirstOrDefault();

        return dbUser;
    }

    public void DeleteUserById(Guid id)
    {
        _db.Container.DeleteItemAsync<DbApplicationUser>(id.ToString(), new PartitionKey(id.ToString()));
    }

    public ApplicationUser FromHttpContext(HttpContext httpContext)
    {
        var user = FindByEmail(httpContext.User.GetEmail());
        if (user is null)
            throw new InvalidOperationException("Could not find user from httpContext");

        return user;
    }

    public void UpdateRoles(Guid applicationUserId, List<Role> applicationUserRoles)
    {
        var itemResponse = _db.Container.PatchItemAsync<DbApplicationUser>(
            applicationUserId.ToString(),
            new PartitionKey(applicationUserId.ToString()),
            new[] { PatchOperation.Add("/roles", applicationUserRoles) }
        ).Result;
    }
}