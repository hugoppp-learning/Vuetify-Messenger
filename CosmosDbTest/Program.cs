using backend.Model;
using System.Text.Json;
using backend.Services;

var endpoint = args[0];
var key = args[1];
var cosmosDb = new CosmosDbService(endpoint, key);

Console.WriteLine("User count: " + cosmosDb.GetAllUsers().Count);
Console.WriteLine("Press any key to insert user");
Console.ReadKey();

var newGuid = Guid.NewGuid();
cosmosDb.InsertUser(new ApplicationUser()
{
    Email = "email", Roles = new List<Role>() { Role.Verified }, Username = "username",
    PasswordHash = "aosdkfaoplsdjfpoasdifp", Id = newGuid
});

var userFromDb = cosmosDb.GetUserById(newGuid);
Console.WriteLine("Inserted:");
Console.WriteLine(JsonSerializer.Serialize(
    userFromDb,
    new JsonSerializerOptions() { WriteIndented = true }
));

Console.WriteLine("User count: " + cosmosDb.GetAllUsers().Count);
Console.WriteLine("Press any key to delete from db");
Console.ReadKey();
cosmosDb.DeleteUserById(newGuid);
Console.WriteLine("User count: " + cosmosDb.GetAllUsers().Count);