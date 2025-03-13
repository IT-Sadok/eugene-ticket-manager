namespace EventService.Domain.RepositoryModels;

public class Event : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartedAt { get; set; }
}