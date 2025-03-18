using EventService.Domain.RepositoryContracts;
using MongoDB.Driver;

namespace EventService.Infrastructure.Database.MongoInitializer;

public class MongoDbInitializer(IMongoDatabase database) : IMongoDbInitializer
{
    private const string Events = "events";
    private const string Orders = "orders";

    public async Task InitializeAsync()
    {
        var collections = (await database.ListCollectionNamesAsync()).ToList();

        if (!collections.Contains(Events))
            await database.CreateCollectionAsync(Events);
        if (!collections.Contains(Orders))
            await database.CreateCollectionAsync(Orders);
    }
}