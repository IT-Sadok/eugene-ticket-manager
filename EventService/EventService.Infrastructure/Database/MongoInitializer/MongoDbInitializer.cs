using EventService.Domain.RepositoryContracts;
using MongoDB.Driver;

namespace EventService.Infrastructure.Database.MongoInitializer;

public class MongoDbInitializer(IMongoDatabase database) : IMongoDbInitializer
{
    private const string Events = "events";

    public async Task InitializeAsync()
    {
        var collections = (await database.ListCollectionNamesAsync()).ToList();

        if (!collections.Contains(Events))
        {
            await database.CreateCollectionAsync(Events);
        }
    }
}