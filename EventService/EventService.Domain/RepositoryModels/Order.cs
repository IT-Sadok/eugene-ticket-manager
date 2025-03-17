using EventService.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EventService.Domain.RepositoryModels;

public class Order: BaseEntity
{
    [BsonRepresentation(BsonType.String)]
    public Guid TicketId { get; set; }
    public OrderStatus Status { get; set; }
}