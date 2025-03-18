using EventService.Domain.RepositoryContracts;
using EventService.Domain.RepositoryModels;
using MongoDB.Driver;

namespace EventService.Infrastructure.Database;

public class EventServiceDbRepository<T>() : IEventServiceDbRepository<T>
    where T : BaseEntity
{
    public readonly IMongoCollection<T> _collection;

    protected EventServiceDbRepository(IMongoDatabase database, string collectionName) : this()
    {
        _collection = database.GetCollection<T>(collectionName);
    }

    public async Task<List<T>> GetAllRecordsAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task CreateAsync(T entity)
    {
        await _collection.InsertOneAsync(entity);
    }
}