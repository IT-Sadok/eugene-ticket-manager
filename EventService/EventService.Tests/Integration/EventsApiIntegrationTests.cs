using System.Net;
using System.Net.Http.Json;
using EventService.Domain.RepositoryModels;
using EventService.Tests.Models;
using FluentAssertions;
using MongoDB.Driver;
using Microsoft.Extensions.DependencyInjection;

namespace EventService.Tests.Integration;

[Collection("EventServiceApiCollection")]
public class EventsApiIntegrationTests : IClassFixture<TestBase>
{
    private readonly HttpClient _client;
    private readonly IMongoDatabase _database;

    public EventsApiIntegrationTests(TestBase fixture)
    {
        _client = fixture.Client;
        _database = fixture.Factory.Services.GetRequiredService<IMongoDatabase>();
    }

    [Fact]
    public async Task FullCycle_CreateEvent_ThenGetIt_ShouldWork()
    {
        var newEvent = new CreateEventRequest
        {
            Title = "Full Cycle Test Event",
            Description = "Integration Test Description",
            StartedAt = DateTime.UtcNow.AddDays(3)
        };

        var createResponse = await _client.PostAsJsonAsync("/events", newEvent);
        
        createResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        await Task.Delay(500);

        var getResponse = await _client.GetAsync("/events");
        getResponse.EnsureSuccessStatusCode();
        
        var events = await getResponse.Content.ReadFromJsonAsync<List<EventResponse>>();

        events.Should().NotBeNull();

        var dbEvents = await _database.GetCollection<Event>("events")
            .Find(e => e.Title == newEvent.Title)
            .ToListAsync();

        dbEvents.Should().NotBeEmpty();
        var dbEvent = dbEvents.First();
        dbEvent.Title.Should().Be(newEvent.Title);
        dbEvent.Description.Should().Be(newEvent.Description);
        dbEvent.StartedAt.Date.Should().Be(newEvent.StartedAt.Date);
    }
} 