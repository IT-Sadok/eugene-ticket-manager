namespace EventService.Domain.RepositoryContracts;

public interface IMongoDbInitializer
{
    Task InitializeAsync();
}