using AutoMapper;
using backend.Model;
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

    public async Task Add(ApplicationUser applicationUser)
    {
        var dbUser = _mapper.Map<DbApplicationUser>(applicationUser);
        await _db.Container.CreateItemAsync(dbUser);
    }

    public async Task<List<ApplicationUser>> GetAllUsers()
    {
        return (await _db.Container.GetItemLinqQueryable<DbApplicationUser>()
            .Where(u => u.Discriminator == Discriminator.User)
            .ToFeedIterator().ReadNextAsync()).Cast<ApplicationUser>().ToList();
    }

    public async Task<ApplicationUser?> FindByUsername(string username)
    {
        var dbUser = (await _db.Container.GetItemLinqQueryable<DbApplicationUser>()
            .Where(u => u.Discriminator == Discriminator.User && u.Username == username)
            .ToFeedIterator().ReadNextAsync()).FirstOrDefault();

        return dbUser;
    }

    public async Task<DbApplicationUser?> FindByEmail(string email)
    {
        var dbUser = (await _db.Container.GetItemLinqQueryable<DbApplicationUser>()
            .Where(u => u.Discriminator == Discriminator.User && u.Email == email)
            .ToFeedIterator().ReadNextAsync()).FirstOrDefault();

        return dbUser;
    }

    public async Task DeleteUserById(Guid id)
    {
        await _db.Container.DeleteItemAsync<DbApplicationUser>(id.ToString(), new PartitionKey(id.ToString()));
    }

    public async Task<ApplicationUser> FromHttpContext(HttpContext httpContext)
    {
        var user = await FindByEmail(httpContext.User.GetEmail());
        if (user is null)
            throw new InvalidOperationException("Could not find user from httpContext");

        return user;
    }

    public Task UpdateRoles(Guid applicationUserId, List<Role> applicationUserRoles)
    {
        return Task.FromResult(_db.Container.PatchItemAsync<DbApplicationUser>(
            applicationUserId.ToString(),
            new PartitionKey(applicationUserId.ToString()),
            new[] { PatchOperation.Add("/roles", applicationUserRoles) }
        ));
    }
}