namespace EventService.Tests.Models;

public class EventResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartedAt { get; set; }
} 