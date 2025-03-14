using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EventService.Domain.RepositoryModels;

public class BaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
}