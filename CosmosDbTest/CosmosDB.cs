using AutoMapper;
using backend.Model;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Azure.Cosmos.Linq;

namespace ConsoleApp2;

public class CosmosDb
{
    private readonly Container _container;
    private readonly Mapper _mapper;

    public CosmosDb(string endpoint, string key)
    {
        var cosmosClient = new CosmosClientBuilder(endpoint, key)
            .WithSerializerOptions(
                new CosmosSerializationOptions() { PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase })
            .Build();

        var database = cosmosClient.CreateDatabaseIfNotExistsAsync("Vuetify-Messenger").Result.Database;
        _container = database.CreateContainerIfNotExistsAsync("Application", "/id").Result.Container;

        _mapper = new Mapper(
            new MapperConfiguration(cfg => cfg.CreateMap<ApplicationUser, DbApplicationUser>()
            ));
    }

    public static class DbDiscriminator
    {
        public const string User = "user";
    }

    private class DbApplicationUser : ApplicationUser
    {
        public string Discriminator => DbDiscriminator.User;
    }

    public void InsertUser(ApplicationUser applicationUser)
    {
        var dbUser = _mapper.Map<DbApplicationUser>(applicationUser);
        _container.UpsertItemAsync(dbUser).Wait();
    }

    public List<ApplicationUser> GetAllUsers()
    {
        return _container.GetItemLinqQueryable<DbApplicationUser>()
            .Where(u => u.Discriminator == DbDiscriminator.User)
            .ToFeedIterator().ReadNextAsync().Result.Cast<ApplicationUser>().ToList();
    }

    public ApplicationUser? GetUserById(Guid id)
    {
        var dbUser = _container.GetItemLinqQueryable<DbApplicationUser>()
            .Where(u => u.Id == id)
            .ToFeedIterator().ReadNextAsync().Result.FirstOrDefault();

        return dbUser;
    }

    public void DeleteUserById(Guid id)
    {
        _container.DeleteItemAsync<DbApplicationUser>(id.ToString(), new PartitionKey(id.ToString()));
    }
}