using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;

namespace backend.Services;

public class CosmosDbService
{
    public readonly Container Container;

    public CosmosDbService(IConfiguration configuration)
    {
        var cosmosClient = new CosmosClientBuilder(configuration["Cosmos:Endpoint"], configuration["Cosmos:Key"])
            .WithSerializerOptions(
                new CosmosSerializationOptions() { PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase })
            .Build();

        var database = cosmosClient.CreateDatabaseIfNotExistsAsync("Vuetify-Messenger").Result.Database;
        Container = database.CreateContainerIfNotExistsAsync("Application", "/id").Result.Container;
    }
}