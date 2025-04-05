namespace EventService.Tests.Models;

public class CreateEventRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartedAt { get; set; }
} 